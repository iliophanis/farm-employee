export const getQueryParams = (props: any): string => {
  const params = new URLSearchParams();
  Object.keys(props).map((k: any) => params.set(k, String(props[k])));
  return params.toString();
};
