import React, { useEffect, useReducer, useCallback, useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { Formik } from 'formik';
import * as Yup from 'yup';
import apiService from '../../shared/apiService';
import {
  cilPlus,
  cilPencil,
  cilTrash
} from '@coreui/icons';
import CIcon from '@coreui/icons-react';
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
  CCardHeader,
  CCardBody,
  CFormSelect,
  CFormSwitch,
  CToast,
  CToastBody,
  CToastClose,
  CFormLabel
} from '@coreui/react-pro';
import AsyncSelect from 'react-select/async';
import '../../costumStyle/stylesCostum.css';

const initialState = {
  data: [],
  loading: false,
  sort: { column: "id", state: "asc" },
  filter: {},
  records: 0,
  itemsPerPage: 10,
  activePage: 1,
  visibleModal: false,
  titleModal: "",
  toast: { visible: false, message: "" },
  errorPost: "",
  initialValues: {
    id: "",
    aName: "",
    eName: "",
    code: "",
    departmentParentId: "",
    departmentType: 0,
    departmentTypeName: '',
    isActive: false,
    departmentParent: {}
  },
};

const reducer = (state, action) => {
  switch (action.type) {
    case "SET_DATA":
      return { ...state, data: action.payload.data, records: action.payload.records };
    case "SET_ITEMS_PER_PAGE":
      return { ...state, itemsPerPage: action.payload.itemsPerPage };
    case "SET_ACTIVE_PAGE":
      return { ...state, activePage: action.payload.activePage };
    case "SET_LOADING":
      return { ...state, loading: action.payload };
    case "SET_SORT":
      return { ...state, sort: action.payload };
    case "SET_FILTER":
      return { ...state, filter: action.payload };
    case "SET_ERROR_MESSAGE":
      return { ...state, errorPost: action.payload.errorPost };
    case "TOGGLE_MODAL":
      return { ...state, visibleModal: action.payload.visible, titleModal: action.payload.title };
    case "SHOW_TOAST":
      return { ...state, toast: { visible: true, message: action.payload } };
    case "HIDE_TOAST":
      return { ...state, toast: { visible: false, message: "" } };
    case "SET_INITIAL_VALUES":
      return {
        ...state, initialValues: {
          id: action.payload.id,
          eName: action.payload.eName,
          aName: action.payload.aName,
          code: action.payload.code,
          isActive: action.payload.isActive,
          departmentParentId: action.payload.departmentParentId,
          departmentType: action.payload.departmentType,
          departmentTypeName: action.payload.departmentTypeName,
          departmentParent: action.payload.departmentParent
        }
      };
    case "RESET_FORM":
      return { ...state, initialValues: initialState.initialValues, errorPost: "" };
    default:
      return state;
  }
};

const Enum_Department_Type = {
  None: 0,
  GeneralDepartment: 1
};

const Departments = () => {
  const { t, i18n } = useTranslation();
  const [state, dispatch] = useReducer(reducer, initialState);

  const queryParams = useMemo(() => ({
    filter: state.filter,
    sort: state.sort,
    page: state.activePage,
    pageSize: state.itemsPerPage,
  }), [state.filter, state.sort, state.activePage, state.itemsPerPage]);

  const departmentSchema = useMemo(() =>
    Yup.object().shape({
      aName: Yup.string().required(t('fieldRequired').replace("{0}", t('arabicName'))),
      eName: Yup.string().required(t('fieldRequired').replace("{0}", t('englishName'))),
      code: Yup.string().required(t('fieldRequired').replace("{0}", t('code'))),
      departmentType: Yup.number().required(t('fieldRequired').replace("{0}", t('departmentType')))
        .notOneOf([Enum_Department_Type.None], t('fieldCannotBeNone').replace("{0}", t('departmentType'))),
      isAdmin: Yup.boolean()
    }), [t]
  );

  const fetchData = useCallback(async () => {
    dispatch({ type: "SET_LOADING", payload: true });
    try {
      const response = await apiService.post("api/Departments/GetDepartments", queryParams);
      dispatch({ type: "SET_DATA", payload: { data: response.data, records: response.totalItems } });
    } catch (error) {
      dispatch({ type: "SHOW_TOAST", payload: t("fieldToFetchData") + error });
    } finally {
      dispatch({ type: "SET_LOADING", payload: false });
    }
  }, [queryParams, apiService, dispatch, t]);

  useEffect(() => {
    void fetchData();
  }, [fetchData]);

  const handleSortChange = useCallback((sorter) => {
    const newSort = { column: sorter.column, state: sorter.state };
    dispatch({ type: "SET_SORT", payload: newSort });
  }, [dispatch]);

  const handleFilterChange = useCallback((value) => {
    dispatch({ type: "SET_FILTER", payload: value });
  }, [dispatch]);

  const handleFilterChangeBoolCol = useCallback((columnKey, value) => {
    const newFilter = { ...state.filter, [columnKey]: value };
    dispatch({ type: "SET_FILTER", payload: newFilter });
  }, [dispatch, state.filter]);

  const handleAdd = useCallback(() => {
    dispatch({ type: "TOGGLE_MODAL", payload: { visible: true, title: t('addDepartment') } });
  }, [t, dispatch]);

  const handleEdit = useCallback(async (item) => {
    try {
      const response = await apiService.get(`api/Departments/GetDepartment?Id=${item.id}`);
      const departmentParent = {
        value: response.departmentParentId,
        label: i18n.language === 'ar' ? response.departmentParent.aName : response.departmentParent.eName,
      };

      dispatch({ type: "TOGGLE_MODAL", payload: { visible: true, title: t('editDepartment') } });
      dispatch({
        type: "SET_INITIAL_VALUES", payload: {
          id: item.id,
          aName: response.aName,
          eName: response.eName,
          code: response.code,
          departmentParentId: response.departmentParentId,
          departmentType: response.departmentType,
          departmentTypeName: response.departmentTypeName,
          isActive: response.isActive,
          departmentParent: departmentParent
        }
      });
    } catch (error) {
      dispatch({ type: "SHOW_TOAST", payload: t("fieldToFetchData") + error });
    }
  }, [state.initialValues, t, apiService, dispatch]);

  const handleSaveChanges = useCallback(async (values) => {
    try {
      const endpoint = values.id ? "api/Departments/UpdateDepartment" : "api/Departments/AddDepartment";
      const res = await apiService.post(endpoint, values);
      return res;
    } catch (error) {
      dispatch({ type: "SHOW_TOAST", payload: t("failedPostData") + error });
    }
  }, [t, apiService, dispatch]);

  const closeModalCU = useCallback(() => {
    dispatch({ type: "TOGGLE_MODAL", payload: { visible: false, title: '' } });
    dispatch({ type: "RESET_FORM" });
  }, [dispatch]);

  const handleConfirmDelete = useCallback(async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/Departments/DeleteDepartment?id=${item.id}`);
        void fetchData();
      }
    } catch (error) {
      dispatch({ type: "SHOW_TOAST", payload: t("fieldToFetchData") + error });
    }
  }, [fetchData, t, dispatch]);

  const handleItemsPerPageChange = useCallback((itemsPerPage) => {
    dispatch({ type: "SET_ITEMS_PER_PAGE", payload: { itemsPerPage } });
    dispatch({ type: "SET_ACTIVE_PAGE", payload: { activePage: 1 } });
  }, [dispatch]);

  const handleActivePageChange = useCallback((activePage) => {
    dispatch({ type: "SET_ACTIVE_PAGE", payload: { activePage } });
  }, [dispatch]);

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

  const columns = useMemo(() => [
    {
      key: 'actions', label: (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
          <CButton onClick={handleAdd} size="sm">
            <CIcon icon={cilPlus} ClassName="nav-icon" />
          </CButton>
        </div>
      ), _style: { width: '6%' }, filter: false, sorter: false,
    },
    {
      key: 'aName',
      label: t('arabicName'),
      _style: { width: '10%' },
      _props: { className: 'columnHeader' }
    },
    {
      key: 'eName',
      label: t('englishName'),
      _props: { className: 'columnHeader' }
    },
    {
      key: 'code',
      label: t('code'),
      _props: { className: 'columnHeader' }
    },
    {
      key: 'departmentType',
      label: t('departmentType'),
      _props: { className: 'columnHeader' }
    },
    {
      key: 'isActive', label: t('isActive'), filter: (_, onChange) => {
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
    }
  ], [t, handleFilterChangeBoolCol, handleAdd]);

  return (
    <>
      <CToast autohide={true} visible={state.toast.visible} color="danger" className="text-white align-items-center">
        <div className="d-flex">
          <CToastBody>{state.toast.message}</CToastBody>
          <CToastClose className="me-2 m-auto" white />
        </div>
      </CToast>
      <CModal
        visible={state.visibleModal}
        onClose={closeModalCU}
        aria-labelledby="AddUserModalLabel"
        backdrop="static"
        size="xl"
      >
        <CModalHeader>
          <CModalTitle id="AddUserModalLabel">{state.titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={state.initialValues}
            validationSchema={departmentSchema}
            onSubmit={async (values, { resetForm }) => {
              const res = await handleSaveChanges(values);
              if (res && res == 1) {
                dispatch({ type: "TOGGLE_MODAL", payload: { visible: false, title: '' } });
                resetForm();
                fetchData();
              } else if (res && res == 0) {
                dispatch({ type: "SET_ERROR_MESSAGE", payload: '' });
              }
            }}
            enableReinitialize={true}
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, setFieldValue }) => (
              <CForm
                className="row g-2"
                onSubmit={handleSubmit}
              >
                <CCol md={6}>
                  <CFormInput
                    type="text"
                    id="aName"
                    label={t('arabicName')}
                    value={values.aName}
                    onChange={handleChange}
                    required
                    onBlur={handleBlur}
                    autoComplete="off"
                    invalid={touched.aName &&  errors.aName}
                  />
                  {touched.aName && errors.aName && (
                    <div className="invalid-feedback">{errors.aName}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormInput
                    type="text"
                    id="eName"
                    label={t('englishName')}
                    value={values.eName}
                    onChange={handleChange}
                    required
                    onBlur={handleBlur}
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
                    id="code"
                    label={t('code')}
                    value={values.code}
                    onChange={handleChange}
                    required
                    onBlur={handleBlur}
                    autoComplete="off"
                    invalid={touched.code && errors.code}
                  />
                  {touched.code && errors.code && (
                    <div className="invalid-feedback">{errors.code}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormSelect
                    id="departmentType"
                    label={t('departmentType')}
                    value={values.departmentType}
                    onChange={(e) => setFieldValue('departmentType', parseInt(e.target.value, 10))}
                    required
                    onBlur={handleBlur}
                    invalid={touched.departmentType && errors.departmentType}
                  >
                    <option value={Enum_Department_Type.None}></option>
                    <option value={Enum_Department_Type.GeneralDepartment}>{t('generalDepartment')}</option>
                  </CFormSelect>
                  {touched.departmentType && errors.departmentType && (
                    <div className="invalid-feedback">{errors.departmentType}</div>
                  )}
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('departmentParent')}</CFormLabel>
                  <AsyncSelect
                    cacheOptions
                    loadOptions={loadOptions}
                    placeholder="Search and select..."
                    noOptionsMessage={() => 'No options available'}
                    defaultOptions
                    value={state.initialValues.departmentParent}
                    onChange={(selectedOption) => dispatch({
                      type: "SET_INITIAL_VALUES",
                      payload: {
                        ...values,
                        departmentParentId: selectedOption ? selectedOption.value : '',
                        departmentParent: selectedOption
                      }
                    })}
                  />
                </CCol>
                <CCol md={6}>
                  <CFormLabel>{t('isActive')}</CFormLabel>
                  <CFormSwitch
                    id="isActive"
                    checked={values.isActive}
                    onChange={handleChange}
                    className="custom-switch"
                    onBlur={handleBlur}
                  />
                </CCol>
                {state.errorPost && (
                  <div className="invalid-modal">{state.errorPost}</div>
                )}
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
              columns={columns}
              items={state.data}
              columnFilter={{ external: true }}
              columnSorter={{ external: true }}
              pagination={{ external: true }}
              loading={state.loading}
              tableProps={{
                responsive: true,
                striped: true,
                hover: true,
              }}
              onSorterChange={(value) => {
                dispatch({ type: "SET_ACTIVE_PAGE", payload: 1 });
                handleSortChange(value);
              }}
              onColumnFilterChange={(filter) => {
                dispatch({ type: "SET_ACTIVE_PAGE", payload: 1 });
                handleFilterChange(filter);
              }}
              itemsPerPage={state.itemsPerPage}
              paginationProps={{
                activePage: state.activePage,
                pages: state.records > 0 ? Math.ceil(state.records / state.itemsPerPage) : 1,
              }}
              onActivePageChange={handleActivePageChange}
              onItemsPerPageChange={handleItemsPerPageChange}
              scopedColumns={{
                actions: (item) => {
                  return (
                    <td style={{ display: 'flex', justifyContent: 'center' }}>
                      <CButton
                        size="sm"
                        onClick={() => handleEdit(item)}
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
                departmentType: (item) => (
                  <td className="text-center">
                    {item.departmentTypeName}
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

export default Departments;
