import React, { useEffect, useState, useRef } from 'react';
import {
  CSmartTable,
  CRow,
  CCard,
  CCardBody,
  CToast,
  CToastBody,
  CToastClose
} from '@coreui/react-pro';
import '../../costumStyle/stylesCostum.css';
import apiService from '../../shared/apiService';
import { useTranslation } from 'react-i18next';

const Privileges = (props) => {
  const { t, i18n } = useTranslation();
  const [colWidths, setColWidths] = useState({});
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [sort, setSort] = useState({ column: 'id', state: 'asc' });
  const [filter, setFilter] = useState({});
  const [records, setRecords] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [activePage, setActivePage] = useState(1);
  const [visibleToast, setVisibleToast] = useState({ visible: false, message: '' });

  const fetchData = async () => {
    setLoading(true);
    try {
      const queryParams = {
        filter,
        sort,
        page: activePage,
        pageSize: itemsPerPage
      };
      const response = await apiService.post('api/Privileges/GetPrivileges', queryParams);
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

  const headersRefs = {
    aName: useRef(null),
    eName: useRef(null),
    aDescription: useRef(null),
    eDescription: useRef(null)
  };

  return (
    <>
      <CToast autohide={true} visible={visibleToast.visible} color="danger" className="text-white align-items-center">
        <div className="d-flex">
          <CToastBody>{visibleToast.message}</CToastBody>
          <CToastClose className="me-2 m-auto" white />
        </div>
      </CToast>
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CSmartTable
              columns={[
                {
                  key: 'aName',
                  label: (<div ref={headersRefs.aName} style={{ whiteSpace: 'nowrap' }} title={t('arabicName')} > {t('arabicName')} </div>),
                  _style: { width: colWidths.aName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'eName',
                  label: (<div ref={headersRefs.eName} style={{ whiteSpace: 'nowrap' }} title={t('englishName')} > {t('englishName')} </div>),
                  _style: { width: colWidths.eName },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'aDescription',
                  label: (<div ref={headersRefs.aDescription} style={{ whiteSpace: 'nowrap' }} title={t('aDescription')} > {t('aDescription')} </div>),
                  _style: { width: colWidths.aDescription },
                  _props: { style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' } },
                },
                {
                  key: 'eDescription',
                  label: (<div ref={headersRefs.eDescription} style={{ whiteSpace: 'nowrap' }} title={t('eDescription')} > {t('eDescription')} </div>),
                  _style: { width: colWidths.eDescription },
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
            />
          </CRow>
        </CCardBody>
      </CCard>
    </>
  );
};

export default Privileges;

