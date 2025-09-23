import React, { useEffect, useState, useRef } from 'react';
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
import { Formik } from 'formik'; // Import Formik components
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
  const [colWidths, setColWidths] = useState({});
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
      department: null,
      positionId: '',
      position: null
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
    aFirstName: useRef(null),
    eFirstName: useRef(null),
    aLastName: useRef(null),
    eLastName: useRef(null),
    userType: useRef(null),
    isBlocked: useRef(null),
    isAdmin: useRef(null)
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
            {({ values, errors, touched, handleChange, handleSubmit, setFieldValue, setFieldTouched }) => (
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
                  <CCol md={4}>
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
                  <CCol md={4}>
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol md={2}>
                    <CFormLabel>{t('isAdmin')}</CFormLabel>
                    <CFormSwitch
                      id="isAdmin"
                      checked={values.isAdmin}
                      onChange={(e) => setFieldValue('isAdmin', e.target.checked)}
                      className="custom-switch"
                    />
                  </CCol>
                  <CCol md={2}>
                    <CFormLabel>{t('Block')}</CFormLabel>
                    <CFormSwitch
                      id="isBlocked"
                      checked={values.isBlocked}
                      onChange={(e) => setFieldValue('isBlocked', e.target.checked)}
                      className="custom-switch"
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
                {
                  key: 'userName',
                  label: (<div ref={headersRefs.userName} style={{ whiteSpace: 'nowrap' }} title={t('userName')} > {t('userName')} </div>),
                  _style: { width: colWidths.userName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'aFirstName',
                  label: (<div ref={headersRefs.aFirstName} style={{ whiteSpace: 'nowrap' }} title={t('aFirstName')} > {t('aFirstName')} </div>),
                  _style: { width: colWidths.aFirstName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'eFirstName',
                  label: (<div ref={headersRefs.eFirstName} style={{ whiteSpace: 'nowrap' }} title={t('eFirstName')} > {t('eFirstName')} </div>),
                  _style: { width: colWidths.eFirstName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'aLastName',
                  label: (<div ref={headersRefs.aLastName} style={{ whiteSpace: 'nowrap' }} title={t('aLastName')} > {t('aLastName')} </div>),
                  _style: { width: colWidths.aLastName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'eLastName',
                  label: (<div ref={headersRefs.eLastName} style={{ whiteSpace: 'nowrap' }} title={t('eLastName')} > {t('eLastName')} </div>),
                  _style: { width: colWidths.eLastName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'userType',
                  label: (<div ref={headersRefs.userType} style={{ whiteSpace: 'nowrap' }} title={t('userType')} > {t('userType')} </div>),
                  _style: { width: colWidths.userType },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'isBlocked',
                  label: (<div ref={headersRefs.isBlocked} style={{ whiteSpace: 'nowrap' }} title={t('isBlocked')} > {t('isBlocked')} </div>),
                  filter: (_, onChange) => {
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
                  _style: { width: colWidths.userType },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'isAdmin',
                  label: (<div ref={headersRefs.isAdmin} style={{ whiteSpace: 'nowrap' }} title={t('isAdmin')} > {t('isAdmin')} </div>),
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
                  _style: { width: colWidths.userType },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
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
