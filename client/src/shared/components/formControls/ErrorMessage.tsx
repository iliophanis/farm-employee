type IMessageProps = {
  title?: string;
  error: string;
};

const ErrorMessage = ({ title = 'Σφάλμα', error }: IMessageProps) => {
  return (
    <div className='flex items-center justify-center'>
      <div className='flex flex-col text-center text-red-700'>
        <div>
          <div>{title}</div>
          <div className='mt-2'>{error}</div>
        </div>
      </div>
    </div>
  );
};

export default ErrorMessage;
