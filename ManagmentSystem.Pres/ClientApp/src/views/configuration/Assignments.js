import React, { useRef, useEffect, useState } from 'react';
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
  CRow,
  CCard,
  CCardBody,
  CToast,
  CToastBody,
  CToastClose,
  CFormSelect,
  CFormLabel,
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
import sharedFunctions from '../../shared/sharedFunctions';
import { useTranslation } from 'react-i18next';
import { Formik } from 'formik';
import * as Yup from 'yup';
import AsyncSelect from 'react-select/async';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

const Assignments = (props) => {
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
  const [refreshPositions, setRefreshPositions] = useState(false);
  const [initialValues, setInitialValues] = useState({
    id: '',
    userId: '',
    userName: '',
    eFullNameUser: '',
    aFullNameUser: '',
    user: null,
    positionId: '',
    ePositionName: '',
    aPositionName: '',
    position: null,
    isActive: false,
    type: 0,
    typeName: '',
    startDate: null,
    endDate: null,
    departmentId: '',
    department: null,
  });

  // Yup Schema
  const assignmentSchema = Yup.object().shape({
    userId: Yup.string().required(t('fieldRequired').replace("{0}", t('user'))),
    positionId: Yup.string().required(t('fieldRequired').replace("{0}", t('position'))),
    departmentId: Yup.string().required(t('fieldRequired').replace("{0}", t('department'))),
    startDate: Yup.date().required(t('fieldRequired').replace("{0}", t('startDateAssignment'))),
    endDate: Yup.date()
      .required(t('fieldRequired').replace("{0}", t('endDateAssignment')))
      .min(Yup.ref('startDate'), t('endDateMustBeAfterStartDate')),
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
      const response = await apiService.post('api/UserPositions/GetAssignments', queryParams);
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
    setTitleModal(t('addAssignment'));
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/UserPositions/GetAssignment?Id=${item.id}`);
      const department = {
        value: response.departmentId,
        label: i18n.language === 'ar' ? response.departmentAName : response.departmentEName,
      };
      const position = {
        value: response.positionId,
        label: i18n.language === 'ar' ? response.aPositionName : response.ePositionName,
      };
      const user = {
        value: response.userId,
        value2: response.userName,
        label: i18n.language === 'ar' ? response.aFullNameUser : response.eFullNameUser,
      };

      const dataRow = {
        id: item.id,
        userId: response.userId,
        userName: response.userName,
        eFullNameUser: response.userFullEName,
        aFullNameUser: response.userFullAName,
        user: user,
        positionId: response.positionId,
        ePositionName: response.ePositionName,
        aPositionName: response.aPositionName,
        position: position,
        isActive: response.isActive,
        type: response.type,
        typeName: response.typeName,
        startDate: response.startDate,
        endDate: response.endDate,
        departmentId: response.departmentId,
        department: department
      };
      setTitleModal(t('editAssignment'));
      setVisibleModal(true);
      return dataRow;
    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    }
  };

  const handleSaveChanges = async (values, { resetForm }) => {
    try {
      const newAssignment = {
        id: values.id,
        userId: values.userId,
        userName: values.user.value2,
        eFullNameUser: values.eFullNameUser,
        aFullNameUser: values.aFullNameUser,
        positionId: values.positionId,
        departmentId: values.departmentId,
        ePositionName: values.ePositionName,
        aPositionName: values.aPositionName,
        isActive: values.isActive,
        type: 0,
        typeName: '',
        startDate: values.startDate,
        endDate: values.endDate,
      };
      if (values.id == null || values.id === '') {
        await apiService.post('api/UserPositions/AddAssignment', newAssignment);
      } else {
        await apiService.put('api/UserPositions/UpdateAssignment', newAssignment);
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
    setInitialValues({
      id: '',
      user: null,
      userId: '',
      userName: '',
      eFullNameUser: '',
      aFullNameUser: '',
      departmentId: '',
      department: null,
      positionId: '',
      position: null,
      ePositionName: '',
      aPositionName: '',
      isActive: false,
      type: 0,
      typeName: '',
      startDate: null,
      endDate: null,
    });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/UserPositions/DeleteAssignment?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedDeleteData') + error });
    }
  };

  const handleFilterChangeCol = (columnKey, value) => {
    setFilter({ ...filter, [columnKey]: value });
  };

  const loadOptionUsers = async (inputValue) => {
    try {
      const response = await apiService.get(`api/Users/GetUsersList?filter=${inputValue}`);
      const mappedResponse = response.map(item => ({
        label: i18n.language === 'ar' ? item.userFullAName : item.userFullEName,
        value: item.id,
        value2: item.userName
      }));

      const newOption = { label: '', value: '', value2: '' };
      return mappedResponse.concat([newOption]);
    } catch (error) {
      console.error('Error loading options:', error);
      return [];
    }
  };

  const loadOptionDepartments = async (inputValue) => {
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
  };

  const loadOptionPositions = (departmentId) => async (inputValue) => {
    try {
      const response = await apiService.get(`api/Positions/GetPositionsByDepList?departmentId=${departmentId}&filter=${inputValue}`);
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
  };

  const headersRefs = {
    userName: useRef(null),
    eFullNameUser: useRef(null),
    aFullNameUser: useRef(null),
    ePositionName: useRef(null),
    aPositionName: useRef(null),
    typeName: useRef(null),
    isActive: useRef(null),
    startDate: useRef(null),
    endDate: useRef(null),
    actions: useRef(null),
  };

  const customStylesDepValidateError = {
    control: (provided, state) => ({
      ...provided,
      borderColor: "red",
      boxShadow: "0 0 0 0,5px red",
      "&:hover": {
        borderColor: "red",
      },
    }),
  };

  const customStylesPosValidateError = {
    control: (provided, state) => ({
      ...provided,
      borderColor: "red",
      boxShadow: "0 0 0 0,5px red",
      "&:hover": {
        borderColor: "red",
      },
    }),
  };
  
  const customStylesUserValidateError = {
    control: (provided, state) => ({
      ...provided,
      borderColor: "red",
      boxShadow: "0 0 0 0,5px red",
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
        backdrop="static"
      >
        <CModalHeader className={props.isRTL ? 'modal-header-rtl' : ''}>
          <CModalTitle>{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={assignmentSchema}
            onSubmit={handleSaveChanges}
            enableReinitialize={true} //Very Important
          >
            {({ values, errors, touched, handleChange, handleSubmit, setFieldValue, setFieldTouched }) => (
              <CForm
                className="row g-3"
                onSubmit={handleSubmit}
              >
                <CCol md={6}>
                  <CFormLabel>{t('user')}</CFormLabel>
                  <AsyncSelect
                    name="userId"
                    cacheOptions
                    loadOptions={loadOptionUsers}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={values.user}
                    onChange={(selectedOption) => {
                      setFieldValue('userId', selectedOption ? selectedOption.value : '');
                      setFieldValue('user', selectedOption);
                    }}
                    onBlur={() => setFieldTouched('userId', true)}
                    styles={touched.userId && errors.userId && customStylesUserValidateError}
                  />
                  {touched.userId && errors.userId && (
                    <div className="invalid-feedback d-block">{errors.userId}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('department')}</CFormLabel>
                  <AsyncSelect
                    name="departmentId"
                    cacheOptions
                    loadOptions={loadOptionDepartments}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={values.department}
                    onChange={(selectedOption) => {
                      setFieldValue('departmentId', selectedOption ? selectedOption.value : '');
                      setFieldValue('department', selectedOption);
                      setFieldValue('positionId', '');
                      setFieldValue('position', null);
                      setRefreshPositions(!refreshPositions);
                    }}
                    onBlur={() => setFieldTouched('departmentId', true)}
                    styles={touched.departmentId && errors.departmentId && customStylesDepValidateError}
                  />
                  {touched.departmentId && errors.departmentId && (
                    <div className="invalid-feedback d-block">{errors.departmentId}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('position')}</CFormLabel>
                  <AsyncSelect
                    name="positionId"
                    key={refreshPositions}
                    cacheOptions
                    loadOptions={loadOptionPositions(values.departmentId)}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={values.position}
                    onChange={(selectedOption) => {
                      setFieldValue('positionId', selectedOption ? selectedOption.value : '');
                      setFieldValue('position', selectedOption);
                    }}
                    onBlur={() => setFieldTouched('positionId', true)}
                    styles={touched.positionId && errors.positionId && customStylesPosValidateError}
                  />
                  {touched.positionId && errors.positionId && (
                    <div className="invalid-feedback d-block">{errors.positionId}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('startDateAssignment')}</CFormLabel>
                  <DatePicker
                    selected={values.startDate ? new Date(values.startDate) : null}
                    onChange={(date) => setFieldValue('startDate', date)}
                    dateFormat="yyyy-MM-dd"
                    className="form-control"
                  />
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('endDateAssignment')}</CFormLabel>
                  <DatePicker
                    selected={values.endDate ? new Date(values.endDate) : null}
                    onChange={(date) => setFieldValue('endDate', date)}
                    dateFormat="yyyy-MM-dd"
                    className="form-control"
                  />
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
              columns={[
                {
                  key: 'actions', label: (
                    <div ref={headersRefs.actions} style={{ display: 'flex', justifyContent: 'center', whiteSpace: 'nowrap' }}>
                      <CButton onClick={handleAdd} size="sm">
                        <CIcon icon={cilPlus} className="nav-icon" />
                      </CButton>
                    </div>
                  ),
                  _style: { width: colWidths.actions },
                  _props: { className: 'column-header-table' },
                  filter: false,
                  sorter: false,
                },
                {
                  key: 'userName',
                  label: (<div ref={headersRefs.userName} style={{ whiteSpace: 'nowrap' }} title={t('userName')} > {t('userName')} </div>),
                  _style: { width: colWidths.userName },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'eFullNameUser',
                  label: (<div ref={headersRefs.eFullNameUser} style={{ whiteSpace: 'nowrap' }} title={t('userFullEName')} > {t('userFullEName')} </div>),
                  _style: { width: colWidths.eFullNameUser },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'aFullNameUser',
                  label: (<div ref={headersRefs.aFullNameUser} style={{ whiteSpace: 'nowrap' }} title={t('userFullAName')} > {t('userFullAName')} </div>),
                  _style: { width: colWidths.aFullNameUser },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'ePositionName',
                  label: (<div ref={headersRefs.ePositionName} style={{ whiteSpace: 'nowrap' }} title={t('positionEName')} > {t('positionEName')} </div>),
                  _style: { width: colWidths.ePositionName },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'aPositionName',
                  label: (<div ref={headersRefs.aPositionName} style={{ whiteSpace: 'nowrap' }} title={t('positionAName')} > {t('positionAName')} </div>),
                  _style: { width: colWidths.aPositionName },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'typeName',
                  label: (<div ref={headersRefs.typeName} style={{ whiteSpace: 'nowrap' }} title={t('assignmentType')} > {t('assignmentType')} </div>),
                  _style: { width: colWidths.typeName },
                  
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
                          handleFilterChangeCol('isActive', selectedValue)
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
                  key: 'startDate',
                  label: (<div ref={headersRefs.startDate} style={{ whiteSpace: 'nowrap' }} title={t('startDateAssignment')} > {t('startDateAssignment')} </div>),
                  filter: (_, onChange) => {
                    return (
                      //<CDatePicker
                      //  value={startDateFilter}
                      //  onChange={(date) => {
                      //    setStartDateFilter(date.toISOString().split('T')[0]);
                      //    handleFilterChangeCol('startDate', date.toISOString().split('T')[0]); // Convert Date to YYYY-MM-DD format
                      //  }}
                      ///>
                      <></>
                    )
                  },
                  _style: { width: colWidths.startDate },
                  
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'endDate',
                  label: (<div ref={headersRefs.endDate} style={{ whiteSpace: 'nowrap' }} title={t('endDateAssignment')} > {t('endDateAssignment')} </div>),
                  filter: (_, onChange) => {
                    return (
                      //<CDatePicker
                      //  value={endDateFilter}
                      //  onChange={(date) => {
                      //    setEndDateFilter(date.toISOString().split('T')[0]);
                      //    handleFilterChangeCol('endDate', date.toISOString().split('T')[0]) // Convert Date to YYYY-MM-DD format
                      //  }}
                      ///>
                      <></>
                    )
                  },
                  _style: { width: colWidths.endDate },
                  
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
                          setTitleModal(t('editAssignment'))
                        }}
                        className="me-2"
                      >
                        <CIcon icon={cilPencil} className="nav-icon" />
                      </CButton>
                      <CButton
                        size="sm"
                        onClick={() => handleConfirmDelete(item)}
                        className="me-2"
                      >
                        <CIcon icon={cilTrash} className="nav-icon" />
                      </CButton>
                    </td>
                  )
                }
              }}
            />
          </CRow>
        </CCardBody>
      </CCard>
    </>
  );
};

export default Assignments;

