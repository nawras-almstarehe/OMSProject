import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  CButton,
  CSmartTable,
  CModal,
  CModalBody,
  CModalFooter,
  CModalHeader,
  CModalTitle,
  CCol,
  CForm,
  CFormInput,
  CRow,
  CCard,
  CCardBody,
  CToast,
  CToastBody,
  CToastClose,
  CFormLabel,
  CFormSelect,
  CFormSwitch
} from '@coreui/react-pro';
import {
  cilPlus,
  cilPencil,
  cilTrash
} from '@coreui/icons';
import CIcon from '@coreui/icons-react';
import '../../costumStyle/stylesCostum.css';
import apiService from '../../shared/apiService';
import { useTranslation } from 'react-i18next';
import { Formik } from 'formik';
import * as Yup from 'yup';
import AsyncSelect from 'react-select/async';

const Positions = (props) => {
  const { t, i18n } = useTranslation();
  const [colWidths, setColWidths] = useState({});
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [sort, setSort] = useState({ column: 'id', state: 'asc' });
  const [filter, setFilter] = useState({});
  const [records, setRecords] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [activePage, setActivePage] = useState(1);
  const [visibleModal, setVisibleModal] = useState(false);
  const [titleModal, setTitleModal] = useState('');
  const [visibleToast, setVisibleToast] = useState({ visible: false, message: '' });
  const [initialValues, setInitialValues] = useState({
    id: '',
    eName: '',
    aName: '',
    departmentId: '',
    department: null,
    isActive: false,
    isLeader: false
  });

  // Yup Schema
  const positionSchema = Yup.object().shape({
    eName: Yup.string().required(t('fieldRequired').replace("{0}", t('englishName'))),
    aName: Yup.string().required(t('fieldRequired').replace("{0}", t('arabicName'))),
    departmentId: Yup.string().required(t('fieldRequired').replace("{0}", t('department'))),
  });

  const fetchData = async () => {
    setLoading(true);
    try {
      const queryParams = {
        filter,
        sort,
        page: activePage,
        pageSize: itemsPerPage
      };
      const response = await apiService.post('api/Positions/GetPositions', queryParams);
      const { data: tableData, totalItems } = response;
      setData(tableData);
      setRecords(totalItems);
    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const widths = {};
    Object.keys(headersRefs).forEach(key => {
      const header = headersRefs[key].current;
      if (header) {
        widths[key] = header.offsetWidth;
      }
    });
    setColWidths(widths);
    fetchData();
  }, [sort, filter, itemsPerPage, activePage]);

  const handleSortChange = (sorter) => {
    setSort({ column: sorter.column, state: sorter.state });
  };

  const handleFilterChange = (value) => {
    setFilter(value);
  };

  const handleAdd = () => {
    setVisibleModal(true);
    setTitleModal(t('addPosition'));
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/Positions/GetPosition?Id=${item.id}`);
      const department = {
        value: response.departmentId,
        label: i18n.language === 'ar' ? response.departmentAName : response.departmentEName,
      };
      const dataRow = {
        id: item.id,
        eName: response.eName,
        aName: response.aName,
        isActive: response.isActive,
        isLeader: response.isLeader,
        departmentId: response.departmentId,
        department: department
      };

      setTitleModal(t('editPosition'));
      setVisibleModal(true);

      return dataRow;

    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    }
  };

  const handleSaveChanges = async (values, { resetForm }) => { // Receive values and resetForm
    try {
        const newPosition = {
          id: values.id,
          ename: values.eName,
          aname: values.aName,
          isActive: values.isActive,
          isLeader: values.isLeader,
          departmentId: values.departmentId
        };

        if (values.id == null || values.id === '') {
          await apiService.post('api/Positions/AddPosition', newPosition);
        } else {
          await apiService.put('api/Positions/UpdatePosition', newPosition);
        }

        setVisibleModal(false);
        resetForm();
        fetchData();
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedPostData') + error });
    }
  };

  const closeModalCU = () => {
    setVisibleModal(false);
    setInitialValues({ id: '', eName: '', aName: '', departmentId: '', department: null });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/Positions/DeletePosition?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedDeleteData') + error });
    }
  };

  const loadOptions = useCallback(async (inputValue) => {
    try {
      const response = await apiService.get(`api/Departments/GetDepartmentsList?filter=${inputValue}`);
      const mappedResponse = response.map(item => ({
        label: i18n.language === 'ar' ? item.aName : item.eName,
        value: item.id,
      }));

      const newOption = { label: '', value: '' };
      return mappedResponse.concat([newOption]);
    } catch (error) {
      console.error('Error loading options:', error);
      return [];
    }
  }, []);

  const headersRefs = {
    aName: useRef(null),
    eName: useRef(null),
    isActive: useRef(null),
    isLeader: useRef(null),
    actions: useRef(null)
  };

  const customStylesDepValidateError = {
    control: (provided, state) => ({
      ...provided,
      borderColor:  "red" ,
      boxShadow:  "0 0 0 0,5px red" ,
      "&:hover": {
        borderColor: "red",
      },
    }),
  };

  return (
    <>
      <CToast autohide={true} visible={visibleToast.visible} color="danger" className="text-white align-items-center">
        <div className="d-flex">
          <CToastBody>{visibleToast.message}</CToastBody>
          <CToastClose className="me-2 m-auto" white />
        </div>
      </CToast>
      <CModal
        visible={visibleModal}
        onClose={closeModalCU}
        aria-labelledby="AddPositionModalLabel"
        backdrop="static"
      >
        <CModalHeader className={props.isRTL ? 'modal-header-rtl' : ''}>
          <CModalTitle id="AddPositionModalLabel">{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={positionSchema}
            onSubmit={handleSaveChanges}
            enableReinitialize={true} //Very Important
          >
            {({ values, errors, touched, handleChange, handleSubmit, setFieldValue, setFieldTouched }) => (
              <CForm
                className="row g-3"
                onSubmit={handleSubmit}
              >
                <CCol md={6}>
                  <CFormInput
                    type="text"
                    id="eName"
                    label={t('englishName')}
                    value={values.eName}
                    onChange={handleChange}
                    autoComplete="off"
                    invalid={touched.eName && errors.eName}
                  />
                  {touched.eName && errors.eName && (
                    <div className="invalid-feedback">{errors.eName}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormInput
                    type="text"
                    id="aName"
                    label={t('arabicName')}
                    value={values.aName}
                    onChange={handleChange}
                    autoComplete="off"
                    invalid={touched.aName && errors.aName}
                  />
                  {touched.aName && errors.aName && (
                    <div className="invalid-feedback">{errors.aName}</div>
                  )}
                </CCol>
                <CCol xs={12}>
                  <CFormLabel>{t('department')}</CFormLabel>
                  <AsyncSelect
                    name="departmentId"
                    cacheOptions
                    loadOptions={loadOptions}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={values.department}
                    onChange={(selectedOption) => {
                      setFieldValue('departmentId', selectedOption ? selectedOption.value : '');
                      setFieldValue('department', selectedOption);
                    }}
                    onBlur={() => setFieldTouched('departmentId', true)}
                    styles={touched.departmentId && errors.departmentId && customStylesDepValidateError}
                  />
                  {touched.departmentId && errors.departmentId && (
                    <div className="invalid-feedback d-block">{errors.departmentId}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('isActive')}</CFormLabel>
                  <CFormSwitch
                    id="isActive"
                    checked={values.isActive}
                    onChange={(e) => setFieldValue('isActive', e.target.checked)}
                    className="custom-switch"
                  />
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('isLeader')}</CFormLabel>
                  <CFormSwitch
                    id="isLeader"
                    checked={values.isLeader}
                    onChange={(e) => setFieldValue('isLeader', e.target.checked)}
                    className="custom-switch"
                  />
                </CCol>
                <CModalFooter>
                  <CButton color="secondary" variant="outline" onClick={closeModalCU}>
                    {t('close')}
                  </CButton>
                  <CButton color="primary" type="submit">
                    {t('saveChanges')}
                  </CButton>
                </CModalFooter>
              </CForm>
            )}
          </Formik>
        </CModalBody>

      </CModal>
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CSmartTable
              //rowClassName={() => 'custom-table-row'}
              columns={[
                {
                  key: 'actions', label: (
                    <div ref={headersRefs.actions} style={{ display: 'flex', justifyContent: 'center' }}>
                      <CButton onClick={handleAdd} size="sm">
                        <CIcon icon={cilPlus} ClassName="nav-icon" />
                      </CButton>
                    </div>
                  ),
                  _style: { width: colWidths.actions },
                  
                  _props: { className: 'column-header-table' },
                  filter: false,
                  sorter: false,
                },
                {
                  key: 'aName',
                  label: (<div ref={headersRefs.aName} style={{ whiteSpace: 'nowrap' }} title={t('arabicName')} > {t('arabicName')} </div>),
                  _style: { width: colWidths.aName },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'eName',
                  label: (<div ref={headersRefs.eName} style={{ whiteSpace: 'nowrap' }} title={t('englishName')} > {t('englishName')} </div>),
                  _style: { width: colWidths.eName },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'isActive',
                  label: (<div ref={headersRefs.isActive} style={{ whiteSpace: 'nowrap' }} title={t('isActive')} > {t('isActive')} </div>),
                  filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('isActive', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
                  _style: { width: colWidths.isActive },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'isLeader',
                  label: (<div ref={headersRefs.isActive} style={{ whiteSpace: 'nowrap' }} title={t('isLeader')} > {t('isLeader')} </div>),
                  filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('isLeader', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
                  _style: { width: colWidths.isLeader },
                  
                  _props: { className: 'column-header-table' },
                }
              ]}
              items={data}
              columnFilter={{ external: true }}
              columnSorter={{ external: true }}
              pagination={{ external: true }}
              loading={loading}
              tableProps={{
                responsive: true,
                striped: true,
                hover: true,
              }}
              onSorterChange={(value) => {
                setActivePage(1)
                handleSortChange(value)
              }}
              onColumnFilterChange={(filter) => {
                setActivePage(1)
                handleFilterChange(filter)
              }}
              itemsPerPage={itemsPerPage}
              paginationProps={{
                activePage,
                pages: records > 0 ? Math.ceil(records / itemsPerPage) : 1,
              }}
              onActivePageChange={(page) => setActivePage(page)}
              onItemsPerPageChange={(pageSize) => {
                setActivePage(1)
                setItemsPerPage(pageSize)
              }}
              scopedColumns={{
                actions: (item) => {
                  return (
                    <td>
                      <CButton
                        size="sm"
                        onClick={async () => {
                          const itemValue = await handleEdit(item);
                          setInitialValues(itemValue)
                          setVisibleModal(true)
                          setTitleModal(t('editPosition'))
                        }}
                        className="me-2"
                      >
                        <CIcon icon={cilPencil} ClassName="nav-icon" />
                      </CButton>
                      <CButton
                        size="sm"
                        onClick={() => handleConfirmDelete(item)}
                        className="me-2"
                      >
                        <CIcon icon={cilTrash} ClassName="nav-icon" />
                      </CButton>
                    </td>
                  )
                },
                isActive: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.isActive)} disabled />
                  </td>
                ),
                isLeader: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.isLeader)} disabled />
                  </td>
                ),
              }}
            />
          </CRow>
        </CCardBody>
      </CCard>
    </>
  );
};

export default Positions;

