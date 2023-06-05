import { useState, useEffect } from 'react';

export function useArray(defaultValue: unknown) {
  const [items, setItems]: any[] = useState(defaultValue || []);

  function add(item: any) {
    setItems((state: any) => [...state, item]);
  }

  function update(item: any, updated: any) {
    setItems((state: any) => {
      const newState = state.slice();
      const idx = newState.indexOf(item);
      if (idx === -1) {
        throw new Error(`item ${item} not found in array ${state}`);
      }
      newState[idx] = updated;
      return newState;
    });
  }

  function updateByIndex(idx: number, updated: any) {
    setItems((state: any) => {
      const newState = state.slice();
      newState[idx] = updated;
      return newState;
    });
  }

  function remove(item: any) {
    setItems((state: any) => {
      const newState = state.slice();
      newState.splice(newState.indexOf(item), 1);
      return newState;
    });
  }

  function removeByIndex(idx: number) {
    setItems((state: any) => {
      const newState = state.slice();
      newState.splice(idx, 1);
      return newState;
    });
  }

  function swap(idxA: number, idxB: number) {
    setItems((state: any) => {
      const newState = state.slice();
      [newState[idxA], newState[idxB]] = [newState[idxB], newState[idxA]];
      return newState;
    });
  }

  function moveUp(idx: number) {
    return swap(idx, idx - 1);
  }

  function moveDown(idx: number) {
    return swap(idx, idx + 1);
  }

  function clear() {
    setItems([]);
  }

  return {
    items,
    moveUp,
    moveDown,
    add,
    update,
    updateByIndex,
    remove,
    removeByIndex,
    swap,
    clear,
  };
}
