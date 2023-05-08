function state() {
  const state: any = {};
  for (let i = 0; i < localStorage.length; i++) {
    const key = localStorage.key(i);
    state[key!] = get(key!);
  }
  return state;
}

function entries() {
  return Object.entries(state());
}

function get(key: string) {
  const value = localStorage.getItem(key);
  return value && JSON.parse(value);
}

function set(key: string, value: any) {
  if (!value) {
    throw new Error('cannot set a null/undefined value' + value);
  }
  localStorage.setItem(key, JSON.stringify(value));
}

function remove(key: string) {
  localStorage.removeItem(key);
}

function clear() {
  localStorage.clear();
}

const store = Object.freeze({
  state: state,
  entries,
  set: set,
  get: get,
  remove: remove,
  clear: clear,
});

export default store;
