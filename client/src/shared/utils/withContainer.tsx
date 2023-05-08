import { NextPage } from 'next';

export const withContainer = (View: any, hook: any) => {
  const props = hook();
  return (
    <>
      <View {...props} />
    </>
  );
};
