import React from 'react';

type IRowProps = {
  caption: string;
  value: string | number | JSX.Element;
};

const Row = ({ caption, value }: IRowProps) => {
  return (
    <div className='p-3 '>
      <div className='flex flex-row items-center text-white'>{caption}</div>
      <div className='text-gray-300'>{value}</div>
    </div>
  );
};

type IDescriptionListProps = {
  items: { caption: string; value: string | number | JSX.Element }[];
};

const DescriptionList = ({ items }: IDescriptionListProps) => {
  return (
    <>
      {items.map((r, index) => (
        <Row value={r.value} key={index} caption={r.caption} />
      ))}
    </>
  );
};

export default DescriptionList;
