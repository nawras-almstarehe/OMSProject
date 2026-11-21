import React, {
  useEffect, useState, useRef,
  useCallback} from 'react';
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
  CFormTextarea,
  CToast,
  CToastBody,
  CToastClose,
  CFormSelect,
  CFormSwitch,
  CFormLabel
} from '@coreui/react-pro';
import {
  cilPlus,
  cilPencil,
  cilTrash,
  cilImage
} from '@coreui/icons';
import CIcon from '@coreui/icons-react';
import MultiImagesUploadModal from "../../components/MultiImagesUploadModal";
import '../../costumStyle/stylesCostum.css';
import apiService from '../../shared/apiService';
import { useTranslation } from 'react-i18next';
import { Formik } from 'formik'; // Import Formik components
import * as Yup from 'yup'; // Import Yup
import AsyncSelect from 'react-select/async';

const Product = (props) => {
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
  const [visibleModalImages, setVisibleModalImages] = useState(false);
  const [initialValues, setInitialValues] = useState({
    id: '',
    eName: '',
    aName: '',
    description: '',
    quantity: 0,
    status: false,
    isDiscount: false,
    categoryId: '',
    category: null
  }); // Managed by Formik
  const [selectedItemId, setSelectedItemId] = useState(null);

  // Yup Schema
  const productSchema = Yup.object().shape({
    eName: Yup.string().required(t('fieldRequired').replace("{0}", t('englishName'))),
    aName: Yup.string().required(t('fieldRequired').replace("{0}", t('arabicName'))),
    description: Yup.string().required(t('fieldRequired').replace("{0}", t('description'))),
    quantity: Yup.number().required(t('fieldRequired').replace("{0}", t('quantity'))),
    status: Yup.boolean(),
    isDiscount: Yup.boolean(),
    categoryId: Yup.string().required(t('fieldRequired').replace("{0}", t('category'))),
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
      const response = await apiService.post('api/Productes/GetProductes', queryParams);
      const { data: tableData, totalItems } = response.data;
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
    setTitleModal(t('addProduct'));
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/Productes/GetProduct?Id=${item.id}`);
      const category = {
        value: response.data.categoryId,
        label: i18n.language === 'ar' ? response.data.categoryAName : response.data.categoryEName,
      };
      const dataRow = {
        id: item.id,
        eName: response.data.eName,
        aName: response.data.aName,
        description: response.data.description,
        quantity: response.data.quantity,
        status: response.data.status,
        isDiscount: response.data.isDiscount,
        categoryId: response.data.categoryId,
        categoryName: response.data.categoryName,
        category: category
      };
      //setDataRow(dataRow); // No need to set DataRow
      setTitleModal(t('editProduct'));
      setVisibleModal(true);

      return dataRow;

    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    }
  };

  const handleSaveChanges = async (values, { resetForm }) => { // Receive values and resetForm
    try {
      const newProduct = {
        id: values.id,
        ename: values.eName,
        aname: values.aName,
        description: values.description,
        quantity: values.quantity,
        status: values.status,
        isDiscount: values.isDiscount,
        categoryId: values.categoryId
      };

      if (values.id == null || values.id === '') {
        await apiService.post('api/Productes/AddProduct', newProduct);
      } else {
        await apiService.put('api/Productes/UpdateProduct', newProduct);
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
      eName: '',
      aName: '',
      description: '',
      quantity: 0,
      status: false,
      isDiscount: false,
      categoryId: '',
      category: null
    });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/Productes/DeleteProduct?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedDeleteData') + error });
    }
  };

  const loadOptions = useCallback(async (inputValue) => {
    try {
      const response = await apiService.get(`api/Categories/GetCategoriesList?filter=${inputValue}`);
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

  const handleShowModalImages = (item) => {
    setSelectedItemId(item.id);
    setVisibleModalImages(true);
  };

  const headersRefs = {
    aName: useRef(null),
    eName: useRef(null),
    description: useRef(null),
    actions: useRef(null),
    quantity: useRef(null),
    status: useRef(null),
    isDiscount: useRef(null),
    categoryName: useRef(null)
  };

  const handleFilterChangeBoolCol = (columnKey, value) => {
    setFilter({ ...filter, [columnKey]: value });
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
        aria-labelledby="AddProductModalLabel"
        backdrop="static"
      >
        <CModalHeader className={props.isRTL ? 'modal-header-rtl' : ''}>
          <CModalTitle id="AddProductModalLabel">{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={productSchema}
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
                <CCol md={4}>
                  <CFormInput
                    type="number"
                    id="quantity"
                    label={t('arabicName')}
                    value={values.quantity}
                    onChange={handleChange}
                    autoComplete="off"
                    invalid={touched.quantity && errors.quantity}
                  />
                  {touched.quantity && errors.quantity && (
                    <div className="invalid-feedback">{errors.quantity}</div>
                  )}
                </CCol>
                <CCol md={4}>
                  <CFormLabel>{t('status')}</CFormLabel>
                  <CFormSwitch
                    id="status"
                    checked={values.status}
                    onChange={(e) => setFieldValue('status', e.target.checked)}
                    className="custom-switch"
                  />
                </CCol>
                <CCol md={4}>
                  <CFormLabel>{t('isDiscount')}</CFormLabel>
                  <CFormSwitch
                    id="isDiscount"
                    checked={values.isDiscount}
                    onChange={(e) => setFieldValue('isDiscount', e.target.checked)}
                    className="custom-switch"
                  />
                </CCol>
                <CCol xs={12}>
                  <CFormLabel>{t('category')}</CFormLabel>
                  <AsyncSelect
                    name="categoryId"
                    cacheOptions
                    loadOptions={loadOptions}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={values.category}
                    onChange={(selectedOption) => {
                      setFieldValue('categoryId', selectedOption ? selectedOption.value : '');
                      setFieldValue('category', selectedOption);
                    }}
                    onBlur={() => setFieldTouched('categoryId', true)}
                    styles={touched.categoryId && errors.categoryId && customStylesDepValidateError}
                  />
                  {touched.categoryId && errors.categoryId && (
                    <div className="invalid-feedback d-block">{errors.categoryId}</div>
                  )}
                </CCol>
                <CCol xs={12}>
                  <CFormTextarea
                    id="description"
                    label={t('description')}
                    rows={3}
                    value={values.description}
                    onChange={handleChange}
                    invalid={touched.description && errors.description}
                  />
                  {touched.description && errors.description && (
                    <div className="invalid-feedback">{errors.description}</div>
                  )}
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
      {visibleModalImages && <MultiImagesUploadModal show={visibleModalImages} handleClose={() => setVisibleModalImages(false)} itemId={selectedItemId} isRTL={props.isRTL} fromProduct={true} />}
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CSmartTable
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
                  sorter: false
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
                  key: 'description',
                  label: (<div ref={headersRefs.description} style={{ whiteSpace: 'nowrap' }} title={t('description')} > {t('description')} </div>),
                  _style: { width: colWidths.description },
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'quantity',
                  label: (<div ref={headersRefs.quantity} style={{ whiteSpace: 'nowrap' }} title={t('quantity')} > {t('quantity')} </div>),
                  _style: { width: colWidths.quantity },
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'status',
                  label: (<div ref={headersRefs.status} style={{ whiteSpace: 'nowrap' }} title={t('status')} > {t('status')} </div>),
                  filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('status', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
                  _style: { width: colWidths.status },

                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'isDiscount',
                  label: (<div ref={headersRefs.isDiscount} style={{ whiteSpace: 'nowrap' }} title={t('isDiscount')} > {t('isDiscount')} </div>),
                  filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('isDiscount', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
                  _style: { width: colWidths.isDiscount },

                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'categoryName',
                  label: (<div ref={headersRefs.categoryName} style={{ whiteSpace: 'nowrap' }} title={t('categoryName')} > {t('categoryName')} </div>),
                  _style: { width: colWidths.categoryName },
                  _props: { className: 'column-header-table' },
                },
                {
                  key: 'actionsAdditional',
                  label: (<></>),
                  _props: { className: 'column-header-table' },
                  filter: false,
                  sorter: false
                },
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
                          setTitleModal(t('editProduct'))
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
                status: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.status)} disabled />
                  </td>
                ),
                isDiscount: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.isDiscount)} disabled />
                  </td>
                ),
                actionsAdditional: (item) => {
                  return (
                    <td>
                      <CButton
                        size="sm"
                        onClick={() => handleShowModalImages(item)}
                        className="me-2"
                      >
                        <CIcon icon={cilImage} ClassName="nav-icon" />
                      </CButton>
                    </td>
                  )
                },
              }}
            />
          </CRow>
        </CCardBody>
      </CCard>
    </>
  );
};

export default Product;

