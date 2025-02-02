import React, { useState } from 'react';
import {
  CButton,
  CModal,
  CModalHeader,
  CModalTitle,
  CModalBody,
  CModalFooter,
} from '@coreui/react';

const ConfirmationModal = ({ show, onConfirm, onCancel }) => {
  return (
    <CModal show={show} onClose={onCancel}>
      <CModalHeader closeButton>
        <CModalTitle>Confirm Action</CModalTitle>
      </CModalHeader>
      <CModalBody>
        Are you sure you want to proceed with this action?
      </CModalBody>
      <CModalFooter>
        <CButton color="secondary" onClick={onCancel}>
          Cancel
        </CButton>
        <CButton color="primary" onClick={onConfirm}>
          Confirm
        </CButton>
      </CModalFooter>
    </CModal>
  );
};

export default ConfirmationModal;
