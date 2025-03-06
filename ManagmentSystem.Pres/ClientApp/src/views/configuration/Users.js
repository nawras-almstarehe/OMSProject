import React, { useEffect, useState, useCallback } from 'react';
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
import {
  cilPlus,
  cilPencil,
  cilTrash
} from '@coreui/icons';
import CIcon from '@coreui/icons-react';
import '../../costumStyle/stylesCostum.css';
import apiService from '../../shared/apiService';
import { useTranslation } from 'react-i18next';
import { Formik, Field, ErrorMessage } from 'formik'; // Import Formik components
import * as Yup from 'yup'; // Import Yup
import AsyncSelect from 'react-select/async';

const Enum_User_Type = {
  None: 0,
  Employee: 1,
  Producer: 2,
  Consumer: 3
};

const Enum_User_Blocked_Type = {
  None: 0,
  ByAdmin: 1,
  BySystem: 2
};

const Users = (props) => {
  const { t, i18n } = useTranslation();
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [sort, setSort] = useState({ column: 'id', state: 'asc' });
  const [filter, setFilter] = useState({});
  const [records, setRecords] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [activePage, setActivePage] = useState(1);
  const [visibleModal, setVisibleModal] = useState(false);
  const [visibleToast, setVisibleToast] = useState({ visible: false, message: '' });
  const [titleModal, setTitleModal] = useState('');
  const [validatePassword, setValidatePassword] = useState('');
  const [errorPost, setErrorPost] = useState('');
  const [refreshPositions, setRefreshPositions] = useState(false);
  const [initialValues, setInitialValues] = useState(
    {
      id: '',
      userName: '',
      aFirstName: '',
      eFirstName: '',
      aLastName: '',
      eLastName: '',
      phoneNumber: '',
      email: '',
      password: '',
      blockedType: 0,
      userType: 0,
      userTypeName: '',
      isBlocked: false,
      isAdmin: false,
      departmentId: '',
      department: {},
      positionId: '',
      position: {}
    }
  );

  const userSchema = Yup.object().shape({
    userName: Yup.string().required(t('fieldRequired').replace("{0}", t('userName'))),
    aFirstName: Yup.string().required(t('fieldRequired').replace("{0}", t('aFirstName'))),
    eFirstName: Yup.string().required(t('fieldRequired').replace("{0}", t('eFirstName'))),
    aLastName: Yup.string().required(t('fieldRequired').replace("{0}", t('aLastName'))),
    eLastName: Yup.string().required(t('fieldRequired').replace("{0}", t('eLastName'))),
    phoneNumber: Yup.string()
      .required(t('fieldRequired').replace("{0}", t('phoneNumber')))
      .matches(/^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$/im, t('fieldInvalid').replace("{0}", t('phoneNumber'))),
    email: Yup.string()
      .email(t('fieldInvalid').replace("{0}", t('email')))
      .required(t('fieldRequired').replace("{0}", t('email'))),
    userType: Yup.number().required(t('fieldRequired').replace("{0}", t('userType')))
      .notOneOf([Enum_User_Type.None], t('fieldCannotBeNone').replace("{0}", t('userType'))),
    isBlocked: Yup.boolean(),
    isAdmin: Yup.boolean(),
    departmentId: Yup.string().required(t('fieldRequired').replace("{0}", t('department'))),
    positionId: Yup.string().required(t('fieldRequired').replace("{0}", t('position'))),
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
      const response = await apiService.post('api/Users/GetUsers', queryParams);
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
    fetchData();
  }, [sort, filter, itemsPerPage, activePage]);

  const handleSortChange = (sorter) => {
    setSort({ column: sorter.column, state: sorter.state });
  };

  const handleFilterChange = (value) => {
    setFilter(value);
  };

  const handleFilterChangeBoolCol = (columnKey, value) => {
    setFilter({ ...filter, [columnKey]: value });
  };

  const handleAdd = () => {
    setVisibleModal(true);
    setTitleModal(t('addUser'));
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/Users/GetUser?Id=${item.id}`);
      const department = {
        value: response.departmentId,
        label: i18n.language === 'ar' ? response.departmentAName : response.departmentEName,
      };
      const position = {
        value: response.positionId,
        label: i18n.language === 'ar' ? response.positionAName : response.positionEName,
      };
      const dataRow = {
        id: item.id,
        userName: response.userName,
        aFirstName: response.aFirstName,
        eFirstName: response.eFirstName,
        aLastName: response.aLastName,
        eLastName: response.eLastName,
        phoneNumber: response.phoneNumber,
        email: response.email,
        password: response.password,
        blockedType: response.blockedType,
        userType: response.userType,
        userTypeName: response.userTypeName,
        isBlocked: response.isBlocked,
        isAdmin: response.isAdmin,
        departmentId: response.departmentId,
        department: department,
        positionId: response.positionId,
        position: position
      };
      setTitleModal(t('editUser'));
      setVisibleModal(true);

      return dataRow;

    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    }
  };

  const handleSaveChanges = async (values) => {
    try {
      const newUser = {
        id: values.id,
        userName: values.userName,
        aFirstName: values.aFirstName,
        eFirstName: values.eFirstName,
        aLastName: values.aLastName,
        eLastName: values.eLastName,
        phoneNumber: values.phoneNumber,
        email: values.email,
        password: values.password,
        blockedType: values.blockedType,
        userType: values.userType,
        isBlocked: values.isBlocked,
        isAdmin: values.isAdmin,
        departmentId: values.departmentId,
        positionId: values.positionId
      };
      var res = {};
      if (values.id == null || values.id == '') {
        res = await apiService.post('api/Users/AddUser', newUser);
      } else {
        res = await apiService.put('api/Users/UpdateUser', newUser);
      }

      return res;
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedPostData') + error });
    }
  };

  const closeModalCU = () => {
    setVisibleModal(false);
    setInitialValues({
      id: '',
      userName: '',
      aFirstName: '',
      eFirstName: '',
      aLastName: '',
      eLastName: '',
      phoneNumber: '',
      email: '',
      password: '',
      blockedType: 0,
      userType: 0,
      userTypeName: '',
      isBlocked: false,
      isAdmin: false,
      departmentId: '',
      department: {},
      positionId: '',
      position: {}
    });
    setValidatePassword('');
    setErrorPost('');
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/Users/DeleteUser?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedDeleteData') + error });
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

  const loadOptionPositions = async (inputValue) => {
    try {
      const response = await apiService.get(`api/Positions/GetPositionsList?departmentId=${initialValues.departmentId}&filter=${inputValue}`);
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
        aria-labelledby="AddUserModalLabel"
        backdrop="static"
        size="xl"
      >
        <CModalHeader>
          <CModalTitle id="AddUserModalLabel">{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={userSchema}
            onSubmit={async (values, { resetForm }) => {
              const res = await handleSaveChanges(values);
              if (res && res.result == 1) {
                setVisibleModal(false);
                resetForm();
                fetchData();
              } else if (res && res.result == 3) {
                setValidatePassword(res.message);
              } else if (res && res.result == 0) {
                setErrorPost(res.message);
              }
            }}
            enableReinitialize={true} //Very Important
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, setFieldValue }) => (
              <CForm onSubmit={handleSubmit}>
                <CRow className="mb-3">
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="userName"
                      label={t('userName')}
                      value={values.userName}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.userName && errors.userName}
                    />
                    {touched.userName && errors.userName && (
                      <div className="invalid-feedback">{errors.userName}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="aFirstName"
                      label={t('aFirstName')}
                      value={values.aFirstName}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.aFirstName && errors.aFirstName}
                    />
                    {touched.aFirstName && errors.aFirstName && (
                      <div className="invalid-feedback">{errors.aFirstName}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="eFirstName"
                      label={t('eFirstName')}
                      value={values.eFirstName}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.eFirstName && errors.eFirstName}
                    />
                    {touched.eFirstName && errors.eFirstName && (
                      <div className="invalid-feedback">{errors.eFirstName}</div>
                    )}
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol md={4}>
                    <CFormInput
                      type="password"
                      id="password"
                      label={t('password')}
                      value={values.password}
                      onChange={handleChange}
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={validatePassword}
                    />
                    {validatePassword && (
                      <div className="invalid-feedback">{validatePassword}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="aLastName"
                      label={t('aLastName')}
                      value={values.aLastName}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.aLastName && errors.aLastName}
                    />
                    {touched.aLastName && errors.aLastName && (
                      <div className="invalid-feedback">{errors.aLastName}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="eLastName"
                      label={t('eLastName')}
                      value={values.eLastName}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.eLastName && errors.eLastName}
                    />
                    {touched.eLastName && errors.eLastName && (
                      <div className="invalid-feedback">{errors.eLastName}</div>
                    )}
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol md={4}>
                    <CFormInput
                      type="text"
                      id="phoneNumber"
                      label={t('phoneNumber')}
                      value={values.phoneNumber}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.phoneNumber && errors.phoneNumber}
                    />
                    {touched.phoneNumber && errors.phoneNumber && (
                      <div className="invalid-feedback">{errors.phoneNumber}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormInput
                      type="email"
                      id="email"
                      label={t('email')}
                      value={values.email}
                      onChange={handleChange}
                      required
                      onBlur={handleBlur}
                      autoComplete="off"
                      invalid={touched.email && errors.email}
                    />
                    {touched.email && errors.email && (
                      <div className="invalid-feedback">{errors.email}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormSelect
                      id="userType"
                      label={t('userType')}
                      value={values.userType}
                      onChange={(e) => setFieldValue('userType', parseInt(e.target.value, 10))}
                      required
                      onBlur={handleBlur}
                      invalid={touched.userType && errors.userType}
                    >
                      <option value={Enum_User_Type.None}></option>
                      <option value={Enum_User_Type.Employee}>{t('employee')}</option>
                      <option value={Enum_User_Type.Producer}>{t('producer')}</option>
                      <option value={Enum_User_Type.Consumer}>{t('consumer')}</option>
                    </CFormSelect>
                    {touched.userType && errors.userType && (
                      <div className="invalid-feedback">{errors.userType}</div>
                    )}
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol md={4}>
                    <CFormLabel>{t('department')}</CFormLabel>
                    <AsyncSelect
                      cacheOptions
                      loadOptions={loadOptionDepartments}
                      placeholder="Search and select..."
                      noOptionsMessage={() => 'No options available'}
                      defaultOptions
                      value={initialValues.department}
                      onChange={(selectedOption) => {
                        setInitialValues({
                          ...values,
                          departmentId: selectedOption ? selectedOption.value : '',
                          department: selectedOption,
                          position: null, // Reset position
                          positionId: ''
                        });
                        setRefreshPositions(!refreshPositions);
                      }}
                    />
                    {touched.departmentId && errors.departmentId && (
                      <div className="invalid-feedback">{errors.departmentId}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                    <CFormLabel>{t('position')}</CFormLabel>
                    <AsyncSelect
                      key={refreshPositions}
                      cacheOptions
                      loadOptions={loadOptionPositions}
                      placeholder="Search and select..."
                      noOptionsMessage={() => 'No options available'}
                      defaultOptions
                      value={initialValues.position}
                      onChange={(selectedOption) => setInitialValues({
                        ...values,
                        positionId: selectedOption ? selectedOption.value : '',
                        position: selectedOption
                      })}
                    />
                    {touched.positionId && errors.positionId && (
                      <div className="invalid-feedback">{errors.positionId}</div>
                    )}
                  </CCol>
                  <CCol md={4}>
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol md={2}>
                    <CFormLabel>{t('isAdmin')}</CFormLabel>
                    <CFormSwitch
                      id="isAdmin"
                      checked={values.isAdmin}
                      onChange={handleChange}
                      className="custom-switch"
                      onBlur={handleBlur}
                    />
                  </CCol>
                  <CCol md={2}>
                    <CFormLabel>{t('Block')}</CFormLabel>
                    <CFormSwitch
                      id="isBlocked"
                      checked={values.isBlocked}
                      onChange={handleChange}
                      className="custom-switch"
                      onBlur={handleBlur}
                    />
                  </CCol>
                </CRow>
                {errorPost && (
                  <div className="invalid-modal">{errorPost}</div>
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
              columns={[
                {
                  key: 'actions', label: (
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
                      <CButton onClick={handleAdd} size="sm">
                        <CIcon icon={cilPlus} ClassName="nav-icon" />
                      </CButton>
                    </div>
                  ), _style: { width: '6%' }, filter: false, sorter: false,
                },
                { key: 'userName', label: t('userName'), _props: { className: 'columnHeader' }, },
                { key: 'aFirstName', label: t('aFirstName'), _props: { className: 'columnHeader' }, },
                { key: 'eFirstName', label: t('eFirstName'), _props: { className: 'columnHeader' }, },
                { key: 'aLastName', label: t('aLastName'), _props: { className: 'columnHeader' }, },
                { key: 'eLastName', label: t('eLastName'), _props: { className: 'columnHeader' }, },
                { key: 'userType', label: t('userType'), _props: { className: 'columnHeader' }, },
                {
                  key: 'isBlocked', label: t('isBlocked'), filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('isBlocked', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
                },
                {
                  key: 'isAdmin',
                  label: t('isAdmin'),
                  _props: { className: 'columnHeader' },
                  filter: (_, onChange) => {
                    return (
                      <CFormSelect
                        size="sm"
                        onChange={(e) => {
                          const selectedValue = e.target.value
                          handleFilterChangeBoolCol('isAdmin', selectedValue)
                        }}
                      >
                        <option value=""></option>
                        <option value={true}>{t('yes')}</option>
                        <option value={false}>{t('no')}</option>
                      </CFormSelect>
                    )
                  },
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
                    <td style={{ display: 'flex', justifyContent: 'center' }}>
                      <CButton
                        size="sm"
                        onClick={async () => {
                          const itemValue = await handleEdit(item);
                          setInitialValues(itemValue)
                          setVisibleModal(true)
                          setTitleModal(t('editUser'))
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
                isBlocked: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.isBlocked)} disabled />
                  </td>
                ),
                isAdmin: (item) => (
                  <td className="text-center">
                    <input type="checkbox" checked={Boolean(item.isAdmin)} disabled />
                  </td>
                ),
                userType: (item) => (
                  <td className="text-center">
                    {item.userTypeName}
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

export default Users
