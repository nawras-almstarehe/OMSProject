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
  CAlert,
  CFormTextarea
} from '@coreui/react-pro'
import {
  cilPlus,
  cilPencil,
  cilTrash,
  cilCheckCircle,
  cilImage
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import MultiImagesUploadModal from "../../components/MultiImagesUploadModal";
import '../../costumStyle/stylesCostum.css'
import apiService from '../../shared/apiService';

const Category = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [sort, setSort] = useState({ column: 'id', state: 'asc' });
  const [filter, setFilter] = useState({});
  const [records, setRecords] = useState(0)
  const [itemsPerPage, setItemsPerPage] = useState(10)
  const [activePage, setActivePage] = useState(1)
  const [visibleModal, setVisibleModal] = useState(false)
  const [visibleModalImages, setVisibleModalImages] = useState(false)
  const [validated, setValidated] = useState(false)
  const [dataRow, setDataRow] = useState({id: '', eName: '', aName: '', description: ''})
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
      const response = await apiService.post('api/Categories/GetCategories', queryParams);
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
        aName: item.aName || '',
        eName: item.eName || '',
        description: item.description || '',
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

  const handleChange = (e) => {
    const { id, value } = e.target;
    setDataRow((prev) => ({
      ...prev,
      [id]: value,
    }));
  };

  const handleAdd = () => {
    setVisibleModal(!visibleModal)
  };

  const handleEdit = async (item) => {
    try {
      const response = await apiService.get(`api/Categories/GetCategory?Id=${item.id}`);
      const dataRow = {
        id: item.id,
        eName: response.eName,
        aName: response.aName,
        description: response.description
      };
      setDataRow(dataRow);
    } catch (error) {
      console.error('Failed to fetch data:', error);
    } finally {
      setVisibleModal(!visibleModal)
    }
  };

  const handleSaveChanges = async (event) => {
    event.preventDefault();
    if (!dataRow.eName || !dataRow.aName || !dataRow.description) {
      setValidated(true);
      alert('Please fill out all required fields.');
      return;
    }
    var newCategory = {};
    if (dataRow.id == null || dataRow.id == '') {
      newCategory = {
        id: dataRow.id,
        ename: dataRow.eName,
        aname: dataRow.aName,
        description: dataRow.description
      };
    } else {
      newCategory = {
        id: dataRow.id,
        ename: dataRow.eName,
        aname: dataRow.aName,
        description: dataRow.description
      };
    }
    try {
      if (dataRow.id == null || dataRow.id == '') {
        const data = await apiService.post('api/Categories/AddCategory', newCategory);
      } else {
        const data = await apiService.put('api/Categories/UpdateCategory', newCategory);
      }
      
      setValidated(false);
      setVisibleModal(false);
      setDataRow({ id: '', eName: '', aName: '', description: '' });
      fetchData();
    } catch (error) {
      console.error("Upload failed:", error);
    }
  };

  const closeModalCU = () => {
    setVisibleModal(false);
    setDataRow({ id: '', eName: '', aName: '', description: '' });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm('Are you sure you want to delete this item?');
      if (confirmDelete) {
        const response = await apiService.delete(`api/Categories/DeleteCategory?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      console.error("Upload failed:", error);
    }
  };

  const handleShowModalImages = (item) => {
    setSelectedItemId(item.id);
    setVisibleModalImages(true);
  };

  return (
    <>
      <CModal
        visible={visibleModal}
        onClose={closeModalCU}
        aria-labelledby="AddCategoryModalLabel"
        backdrop="static"
      >
        <CModalHeader>
          <CModalTitle id="AddCategoryModalLabel">Add Category</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <CForm
            className="row g-3 needs-validation"
            noValidate
            validated={validated}
          >
            <CCol md={6}>
              <CFormInput
                type="text"
                id="eName"
                label="English Name"
                value={dataRow.eName}
                onChange={handleChange}
                required
              />
            </CCol>
            <CCol md={6}>
              <CFormInput
                type="text"
                id="aName"
                label="Arabic Name"
                value={dataRow.aName}
                onChange={handleChange}
                required
              />
            </CCol>
            <CCol xs={12}>
              <CFormTextarea
                id="description"
                label="Description"
                rows={3}
                value={dataRow.description}
                onChange={handleChange}
                required
              />
            </CCol>
          </CForm>
        </CModalBody>
        <CModalFooter>
          <CButton color="secondary" onClick={closeModalCU}>
            Close
          </CButton>
          <CButton color="primary" onClick={handleSaveChanges}>
            Save Changes
          </CButton>
        </CModalFooter>
      </CModal>
      {visibleModalImages && <MultiImagesUploadModal show={visibleModalImages} handleClose={() => setVisibleModalImages(false)} itemId={selectedItemId} />}
      <CCard className="mb-4">
        <CCardHeader>Category</CCardHeader>
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
                  ), _style: { width: '20%' }, filter: false, sorter: false,
                },
                { key: 'aName', label: 'AName', _props: { className: 'columnHeader' }, },
                { key: 'eName', label: 'EName', _props: { className: 'columnHeader' }, },
                { key: 'description', label: 'Description', _props: { className: 'columnHeader' }, },
                {
                  key: 'actionsAdditional', label: (<></>), _style: { width: '20%' }, filter: false, sorter: false,
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
                actionsAdditional: (item) => {
                  return (
                    <td style={{ display: 'flex', justifyContent: 'center' }}>
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
  )
}

export default Category
