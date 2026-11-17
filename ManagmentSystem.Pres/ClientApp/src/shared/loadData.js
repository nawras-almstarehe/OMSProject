const loadData = async (page, pageSize, column, order) => {
  const queryParams = new URLSearchParams({
    page: pagination.page,
    pageSize: pagination.pageSize,
    filter: filter || '',
    order: sortInfo.order || '',
    sort: sortInfo.column || '',
  }).toString();
  const response = await apiService.get(`api/Categories/GetCategories?${queryParams}`);
  return response;
};

export default loadData;
