import React, { useRef, useEffect, useState } from 'react';
import { CSmartTable } from '@coreui/react-pro';

const OMSTable = ({ columns, items, loading, onSorterChange, onColumnFilterChange, paginationProps, onActivePageChange, onItemsPerPageChange, scopedColumns }) => {
  const [colWidths, setColWidths] = useState({});

  // Create refs for each column key dynamically
  const headersRefs = useRef({});

  // Initialize refs for columns if not already there
  if (columns) {
    columns.forEach(col => {
      if (!headersRefs.current[col.key]) headersRefs.current[col.key] = React.createRef();
    });
  }

  // Measure header widths on mount and when columns change
  useEffect(() => {
    const widths = {};
    Object.keys(headersRefs.current).forEach(key => {
      const header = headersRefs.current[key].current;
      if (header) {
        widths[key] = header.offsetWidth;
      }
    });
    setColWidths(widths);
  }, [columns, items]);

  // Enhance columns with measured widths and wrapped labels with refs & ellipsis styles
  const enhancedColumns = columns.map(col => {
    return {
      ...col,
      label: (
        <div
          ref={headersRefs.current[col.key]}
          style={{ whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis', backgroundColor: '#bfbfbf', cursor: 'default' }}
          title={typeof col.label === 'string' ? col.label : undefined} // Show tooltip only if label is string
        >
          {col.label}
        </div>
      ),
      _style: { ...col._style, width: colWidths[col.key] || 'auto' },
      _props: {
        ...(col._props || {}),
        style: { whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis', backgroundColor: '#bfbfbf', ...(col._props?.style || {}) }
      },
    };
  });

  return (
    <CSmartTable
      columns={enhancedColumns}
      items={items}
      loading={loading}
      columnSorter={{ external: true }}
      columnFilter={{ external: true }}
      pagination={{ external: true }}
      onSorterChange={onSorterChange}
      onColumnFilterChange={onColumnFilterChange}
      paginationProps={paginationProps}
      onActivePageChange={onActivePageChange}
      onItemsPerPageChange={onItemsPerPageChange}
      scopedColumns={scopedColumns}
      tableProps={{
        responsive: true,
        striped: true,
        hover: true,
      }}
    />
  );
};

export default OMSTable;
