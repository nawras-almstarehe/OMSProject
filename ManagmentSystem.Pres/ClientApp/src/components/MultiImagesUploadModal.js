import React, { useState, useEffect } from "react";
import { CModal, CModalHeader, CModalTitle, CModalBody, CModalFooter, CButton, CFormInput, CRow, CCol } from "@coreui/react";
import apiService from '../shared/apiService';
import { useTranslation } from 'react-i18next';

const MultiImagesUploadModal = ({ show, handleClose, itemId, isRTL }) => {
  const [selectedFiles, setSelectedFiles] = useState([]);
  const [images, setImages] = useState([]);
  const { t, i18n } = useTranslation()

  useEffect(() => {
    fetchData();
  }, [show, itemId]);

  const fetchData = async () => {
    try {
      if (show && itemId) {
        const response = await apiService.get(`api/Images/GetImagesByCategoryId?categoryId=${itemId}`);
        setImages(response)
      }
    } catch (error) {
      console.error('Failed to fetch data:', error);
    } finally {
    }
  };

  const handleFileChange = (event) => {
    setSelectedFiles([...event.target.files]);
  };

  const handleUpload = async () => {
    const formData = new FormData();
    selectedFiles.forEach(file => {
      formData.append("files", file);
    });
    formData.append("itemId", itemId);

    try {
      const response = await apiService.postWithMedia('api/Images/Upload', formData);
      
      setImages([...images, ...response.data]); // Update UI
      setSelectedFiles([]); // Clear file selection
    } catch (error) {
      console.error("Upload failed:", error);
    }
  };

  const handleDelete = async (imageId) => {
    try {
      const confirmDelete = window.confirm('Are you sure you want to delete this item?');
      if (confirmDelete) {
        const response = await apiService.delete(`api/Images/Delete?imageId=${imageId}`);
        if (response == 1) {
          setImages(images.filter(img => img.id !== imageId));
        }
      }
    } catch (error) {
      console.error("Error deleting image:", error);
    }
  };

  return (
    <CModal visible={show} onClose={handleClose} size="lg" backdrop="static">
      <CModalHeader>
        <CModalTitle>{t('editImages')}</CModalTitle>
      </CModalHeader>
      <CModalBody>
        <CFormInput
          type="file"
          multiple
          onChange={handleFileChange}
          label={t('selectImages')}
          lang={i18n.language}
          dir={isRTL ? 'rtl' : 'ltr'}
        />

        <CButton color="primary" className="mt-3" onClick={handleUpload} disabled={selectedFiles.length === 0}>
          {t('uploadImages') }
        </CButton>

        <h5 className="mt-4">{t('uploadedImages')}</h5>
        <CRow>
          {images.map((img) => (
            <CCol xs="4" md="3" key={img.id} className="position-relative text-center">
              <img src={img.imagePath} alt="Uploaded" width="100%" height="100" className="border rounded" />
              <CButton
                color="danger"
                size="sm"
                className="position-absolute top-0 end-0"
                onClick={() => handleDelete(img.id)}
              >
                ✕
              </CButton>
            </CCol>
          ))}
        </CRow>
      </CModalBody>
      <CModalFooter>
        <CButton color="secondary" onClick={handleClose}>{t('close')}</CButton>
      </CModalFooter>
    </CModal>
  );
};

export default MultiImagesUploadModal;
