export function delay(
  setLoading: (load: boolean) => void,
  delay: number,
  execute: any
) {
  setLoading(true);
  return setTimeout(() => {
    setLoading(false);
    execute();
  }, delay);
}
