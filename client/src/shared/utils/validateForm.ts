export const handleValidateForm = (data: any) => {
  let isValid = true;
  Object.keys(data).map((d) => {
    if (data[d] === '') return (isValid = false);
    return true;
  });
  return isValid;
};
