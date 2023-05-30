import React from 'react';

type IRowProps = {
  caption: string;
  value: string | number;
};

const Row = ({ caption, value }: IRowProps) => {
  return (
    <div className='space-y-1 border-b p-6  md:grid md:grid-cols-2 md:space-y-0'>
      <p className='flex flex-row items-center text-gray-100'>{caption}</p>
      <p className='text-gray-400'>{value}</p>
    </div>
  );
};

type IDescriptionListProps = {
  items: { caption: string; value: string | number }[];
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
