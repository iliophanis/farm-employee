import { FcGoogle } from 'react-icons/fc';
type IProps = {
  handleGoogleLogin: any;
};
const GoogleButton = ({ handleGoogleLogin }: IProps) => {
  return (
    <button
      onClick={(e: any) => {
        e.preventDefault();
        handleGoogleLogin();
      }}
      className='mt-2 flex w-full transform items-center justify-center rounded-md border border-transparent border-transparent border-red-600 bg-white py-2  px-4 text-sm font-medium text-red-600  transition duration-500 ease-in-out hover:bg-gray-300'
    >
      <FcGoogle className='mr-2' /> Σύνδεση μέσω Google
    </button>
  );
};

export default GoogleButton;
