import axios from 'axios';

const axiosInstance = (
  token: string | undefined,
  contentType = 'application/json'
) => {
  let headers = {
    'Content-Type': contentType,
  };
  if (token)
    headers = Object.assign(headers, { Authorization: `Bearer ${token}` });
  return axios.create({
    baseURL: `${process.env.NEXT_PUBLIC_API_URI}/api`,
    headers: headers,
  });
};

const responseBody = (response: any) => response.data;
const errorBody = (error: any) => error;
const customAxios = {
  get: (url: string, token?: string, config?: any) =>
    axiosInstance(token).get(url, config).then(responseBody).catch(errorBody),
  post: (url: string, body: any, token?: string, config?: any) =>
    axiosInstance(token)
      .post(url, body, config)
      .then(responseBody)
      .catch(errorBody),
  put: (url: string, body: any, token?: string, config?: any) =>
    axiosInstance(token)
      .put(url, body, config)
      .then(responseBody)
      .catch(errorBody),
  del: (url: string, token?: string, config?: any) =>
    axiosInstance(token)
      .delete(url, config)
      .then(responseBody)
      .catch(errorBody),
  postFile: (url: string, file: Blob, token?: string, config?: any) => {
    const formData = new FormData();
    formData.append('file', file);
    return axiosInstance(token, 'multipart/form-data')
      .post(url, formData, config)
      .then(responseBody)
      .catch(errorBody);
  },
  postFormData: (
    url: string,
    formData: FormData,
    setPercent: any,
    token?: string,
    config?: any
  ) => {
    return axiosInstance(token, 'multipart/form-data')
      .post(url, formData, {
        ...config,
        onUploadProgress: (event: any) => {
          const progress = Math.round((100 * event.loaded) / event.total);
          console.log(progress + '%');
          setPercent(progress);
        },
      }) //TODO CHECK CONFIG
      .then(responseBody)
      .catch(errorBody);
  },
  downloadFile: (url: string, token?: string, config?: any) =>
    axiosInstance(token)
      .get(url, { ...config, responseType: 'blob' }) //TODO CHECK CONFIG
      .then(responseBody)
      .catch(errorBody),
};

export default customAxios;