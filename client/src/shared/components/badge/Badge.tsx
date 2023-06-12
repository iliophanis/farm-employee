import React from 'react';
type IProps = { label: string };
const Badge = ({ label }: IProps) => {
  return (
    <div className='mr-2 rounded border border-primary-500 bg-purple-100 px-2.5 py-0.5 text-xs font-medium text-primary-900 dark:bg-gray-700 dark:text-primary-900'>
      {label}
    </div>
  );
};

export default Badge;
