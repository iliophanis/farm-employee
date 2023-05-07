const convertToDate = (date: string | Date) => {
  const d = new Date(date);
  const dateStr = d.toLocaleDateString('el-GR');
  return dateStr;
};

const convertToDateTime = (date: string | Date) => {
  const d = new Date(date);
  const dateStr = d.toLocaleString('el-GR');
  return dateStr;
};

const dateUtils = {
  convertToDate,
  convertToDateTime,
};

export default dateUtils;
