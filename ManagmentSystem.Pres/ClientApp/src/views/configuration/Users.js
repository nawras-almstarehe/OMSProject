import React, { useEffect, useState, createRef } from 'react'
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
  CFormSwitch
} from '@coreui/react-pro'
import {
  cilPlus,
  cilPencil,
  cilTrash
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import '../../costumStyle/stylesCostum.css'
import apiService from '../../shared/apiService';
import { useTranslation } from 'react-i18next';

const Enum_User_Type = {
  None: '0',
  Employee: '1',
  Producer: '2',
  Consumer: '3'
};

const Enum_User_Blocked_Type = {
  None: '0',
  ByAdmin: '1',
  BySystem: '2'
};

const Users = (props) => {
  const { t, i18n } = useTranslation()
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [sort, setSort] = useState({ column: 'id', state: 'asc' });
  const [filter, setFilter] = useState({});
  const [records, setRecords] = useState(0)
  const [itemsPerPage, setItemsPerPage] = useState(10)
  const [activePage, setActivePage] = useState(1)
  const [visibleModal, setVisibleModal] = useState(false)
  const [titleModal, setTitleModal] = useState('')
  const [validated, setValidated] = useState(false)
  const [dataRow, setDataRow] = useState(
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
      blockedType: '',
      userType: '',
      isBlocked: false,
      isAdmin: false
    }
  )
  const [selectedItemId, setSelectedItemId] = useState(null);

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
      console.error('Failed to fetch data:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const preprocessData = (data) => {
      return data.map((item) => ({
        ...item,
        userName: item.userName || '',
        aFirstName: item.aFirstName || '',
        eFirstName: item.eFirstName || '',
        aLastName: item.aLastName || '',
        eLastName: item.eLastName || '',
        phoneNumber: item.phoneNumber || '',
        email: item.email || '',
        password: item.password || '',
        blockedType: item.blockedType || '',
        userType: item.userType || '',
        isBlocked: item.isBlocked || false,
        isAdmin: item.isAdmin || false,
      }));
    };
    setData(preprocessData(data));
    fetchData();
  }, [sort, filter, itemsPerPage, activePage]);

  const handleSortChange = (sorter) => {
    setSort({ column: sorter.column, state: sorter.state })
  };

  const handleFilterChange = (value) => {
    setFilter(value);
  };

  const handleFilterChangeBoolCol = (columnKey, value) => {
    setFilter({ ...filter, [columnKey]: value });
  };

  const handleChange = (e) => {
    const { id, value } = e.target;
    setDataRow((prev) => ({
      ...prev,
      [id]: value,
    }));
  };

  const handleAdd = () => {
    setVisibleModal(!visibleModal)
    setTitleModal(t('addUser'));
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/Users/GetUser?Id=${item.id}`);
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
        isBlocked: response.isBlocked,
        isAdmin: response.isAdmin,
      };
      setDataRow(dataRow);
      setTitleModal(t('editUser'));
    } catch (error) {
      console.error('Failed to fetch data:', error);
    } finally {
      setVisibleModal(!visibleModal)
    }
  };

  const handleSaveChanges = async (event) => {
    event.preventDefault();
    if (!dataRow.userName ||
      !dataRow.aFirstName ||
      !dataRow.eFirstName ||
      !dataRow.aLastName ||
      !dataRow.eLastName ||
      !dataRow.phoneNumber ||
      !dataRow.email ||
      !dataRow.userType
    ) {
      setValidated(true);
      alert('Please fill out all required fields.');
      return;
    }
    var newUser = {
      id: dataRow.id,
      userName: dataRow.userName,
      aFirstName: dataRow.aFirstName,
      eFirstName: dataRow.eFirstName,
      aLastName: dataRow.aLastName,
      eLastName: dataRow.eLastName,
      phoneNumber: dataRow.phoneNumber,
      email: dataRow.email,
      password: dataRow.password,
      blockedType: dataRow.blockedType,
      userType: dataRow.userType,
      isBlocked: dataRow.isBlocked,
      isAdmin: dataRow.isAdmin,
    };
    try {
      if (dataRow.id == null || dataRow.id == '') {
        const data = await apiService.post('api/Users/AddUser', newUser);
      } else {
        const data = await apiService.put('api/Users/UpdateUser', newUser);
      }

      setValidated(false);
      setVisibleModal(false);
      setDataRow({
        id: '',
        userName: '',
        aFirstName: '',
        eFirstName: '',
        aLastName: '',
        eLastName: '',
        phoneNumber: '',
        email: '',
        password: '',
        blockedType: '',
        userType: '',
        isBlocked: false,
        isAdmin: false
      });
      fetchData();
    } catch (error) {
      console.error("Upload failed:", error);
    }
  };

  const closeModalCU = () => {
    setVisibleModal(false);
    setDataRow({
      id: '',
      userName: '',
      aFirstName: '',
      eFirstName: '',
      aLastName: '',
      eLastName: '',
      phoneNumber: '',
      email: '',
      password: '',
      blockedType: '',
      userType: '',
      isBlocked: false,
      isAdmin: false
    });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm('Are you sure you want to delete this item?');
      if (confirmDelete) {
        const response = await apiService.delete(`api/Users/DeleteUser?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      console.error("Upload failed:", error);
    }
  };

  return (
    <>
      <CModal
        visible={visibleModal}
        onClose={closeModalCU}
        aria-labelledby="AddUserModalLabel"
        backdrop="static"
        size="xl"
      //className={props.isRTL ? 'text-end' : 'text-start'}
      >
        <CModalHeader>
          <CModalTitle id="AddUserModalLabel">{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <CForm className="needs-validation" noValidate validated={validated}>
            <CRow className="mb-3">
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="userName"
                  label={t('userName')}
                  value={dataRow.userName}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="aFirstName"
                  label={t('aFirstName')}
                  value={dataRow.aFirstName}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="eFirstName"
                  label={t('eFirstName')}
                  value={dataRow.eFirstName}
                  onChange={handleChange}
                  required
                />
              </CCol>
            </CRow>
            <CRow className="mb-3">
              <CCol md={4}>
                <CFormInput
                  type="password"
                  id="password"
                  label={t('password')}
                  value={dataRow.password}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="aLastName"
                  label={t('aLastName')}
                  value={dataRow.aLastName}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="eLastName"
                  label={t('eLastName')}
                  value={dataRow.eLastName}
                  onChange={handleChange}
                  required
                />
              </CCol>
            </CRow>
            <CRow className="mb-3">
              <CCol md={4}>
                <CFormInput
                  type="text"
                  id="phoneNumber"
                  label={t('phoneNumber')}
                  value={dataRow.phoneNumber}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormInput
                  type="email"
                  id="email"
                  label={t('email')}
                  value={dataRow.email}
                  onChange={handleChange}
                  required
                />
              </CCol>
              <CCol md={4}>
                <CFormSelect
                  id="userType"
                  label={t('userType')}
                  value={dataRow.userType}
                  onChange={handleChange}
                  required
                >
                  <option value=""></option>
                  <option value="employee">{t('employee')}</option>
                  <option value="producer">{t('producer')}</option>
                  <option value="consumer">{t('consumer')}</option>
                </CFormSelect>
              </CCol>
            </CRow>
            <CRow className="mb-3">
              <CCol md={2}>
                <CFormSwitch
                  id="isAdmin"
                  label={t('isAdmin')}
                  checked={dataRow.isAdmin}
                  onChange={(e) => setDataRow({ ...dataRow, isAdmin: e.target.checked })}
                  className="custom-switch"
                />
              </CCol>
              <CCol md={2}>
                <CFormSwitch
                  id="isBlocked"
                  label={t('Block')}
                  checked={dataRow.isBlocked}
                  onChange={(e) => setDataRow({ ...dataRow, isBlocked: e.target.checked })}
                  className="custom-switch"
                />
              </CCol>
            </CRow>
          </CForm>
        </CModalBody>
        <CModalFooter>
          <CButton color="secondary" variant="outline" onClick={closeModalCU}>
            {t('close')}
          </CButton>
          <CButton color="primary" onClick={handleSaveChanges}>
            {t('saveChanges')}
          </CButton>
        </CModalFooter>
      </CModal>
      <CCard className="mb-4">
        <CCardHeader>{t('users')}</CCardHeader>
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
              }}
            />
          </CRow>
        </CCardBody>
      </CCard>
    </>
  )
}

export default Users
