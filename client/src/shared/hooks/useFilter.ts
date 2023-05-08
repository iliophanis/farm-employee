import { useEffect, useState } from 'react';

const useFilter = (items: any, searchField = 'name') => {
  const [filter, setFilter] = useState('');
  const [loading, setLoading] = useState(false);
  const [filteredItems, setFilteredItems] = useState(items);
  useEffect(() => {
    const handlegetItems = () => {
      if (filter === '') return setFilteredItems(items);
      setLoading(true);
      const search = filter.toUpperCase();
      const newItems = items.filter((i: any) =>
        i[searchField].toUpperCase().includes(search)
      );
      setFilteredItems(newItems);
      setLoading(false);
    };
    handlegetItems();
  }, [filter, items, searchField]);

  useEffect(() => {
    setFilteredItems(items);
  }, [items]);

  return { loading, filteredItems, filter, setFilter };
};

export default useFilter;
