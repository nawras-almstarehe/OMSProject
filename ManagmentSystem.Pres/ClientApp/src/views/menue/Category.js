import React, { useEffect, useState } from 'react';
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
  CFormTextarea,
  CToast,
  CToastBody,
  CToastClose
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
import { Formik, Field, ErrorMessage } from 'formik'; // Import Formik components
import * as Yup from 'yup'; // Import Yup

const Category = (props) => {
  const { t, i18n } = useTranslation();
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
  const [initialValues, setInitialValues] = useState({ id: '', eName: '', aName: '', description: '' }); // Managed by Formik
  const [selectedItemId, setSelectedItemId] = useState(null);

  // Yup Schema
  const categorySchema = Yup.object().shape({
    eName: Yup.string().required(t('fieldRequired').replace("{0}", t('englishName'))),
    aName: Yup.string().required(t('fieldRequired').replace("{0}", t('arabicName'))),
    description: Yup.string().required(t('fieldRequired').replace("{0}", t('description'))),
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
      const response = await apiService.post('api/Categories/GetCategories', queryParams);
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

  const handleAdd = () => {
    setVisibleModal(true);
    setTitleModal(t('addCategory'));
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
      //setDataRow(dataRow); // No need to set DataRow
      setTitleModal(t('editCategory'));
      setVisibleModal(true);

      return dataRow;

    } catch (error) {
      setVisibleToast({ visible: true, message: t('fieldToFetchData') + error });
    }
  };

  const handleSaveChanges = async (values, { resetForm }) => { // Receive values and resetForm
    try {
      const newCategory = {
        id: values.id,
        ename: values.eName,
        aname: values.aName,
        description: values.description
      };

      if (values.id == null || values.id === '') {
        await apiService.post('api/Categories/AddCategory', newCategory);
      } else {
        await apiService.put('api/Categories/UpdateCategory', newCategory);
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
    setInitialValues({ id: '', eName: '', aName: '', description: '' });
  };

  const handleConfirmDelete = async (item) => {
    try {
      const confirmDelete = window.confirm(t('deleteConfirmation'));
      if (confirmDelete) {
        await apiService.delete(`api/Categories/DeleteCategory?id=${item.id}`);
        fetchData();
      }
    } catch (error) {
      setVisibleToast({ visible: true, message: t('failedDeleteData') + error });
    }
  };

  const handleShowModalImages = (item) => {
    setSelectedItemId(item.id);
    setVisibleModalImages(true);
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
        aria-labelledby="AddCategoryModalLabel"
        backdrop="static"
      >
        <CModalHeader className={props.isRTL ? 'modal-header-rtl' : ''}>
          <CModalTitle id="AddCategoryModalLabel">{titleModal}</CModalTitle>
        </CModalHeader>
        <CModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={categorySchema}
            onSubmit={handleSaveChanges}
            enableReinitialize={true} //Very Important
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit }) => (
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
                    id="aName"
                    label={t('arabicName')}
                    value={values.aName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    autoComplete="off"
                    invalid={touched.aName && errors.aName}
                  />
                  {touched.aName && errors.aName && (
                    <div className="invalid-feedback">{errors.aName}</div>
                  )}
                </CCol>
                <CCol xs={12}>
                  <CFormTextarea
                    id="description"
                    label={t('description')}
                    rows={3}
                    value={values.description}
                    onChange={handleChange}
                    onBlur={handleBlur}
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
      {visibleModalImages && <MultiImagesUploadModal show={visibleModalImages} handleClose={() => setVisibleModalImages(false)} itemId={selectedItemId} isRTL={props.isRTL} />}
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
                { key: 'aName', label: t('arabicName'), _props: { className: 'columnHeader' }, },
                { key: 'eName', label: t('englishName'), _props: { className: 'columnHeader' }, },
                { key: 'description', label: t('description'), _props: { className: 'columnHeader' }, },
                {
                  key: 'actionsAdditional', label: (<></>), _style: { width: '10%' }, filter: false, sorter: false,
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
                          setTitleModal(t('editCategory'))
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
  );
};

export default Category;

