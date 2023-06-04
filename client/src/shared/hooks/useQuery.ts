import {
  QueryFunction,
  QueryKey,
  useMutation,
  useQuery as useQueryBase,
  useQueryClient,
} from '@tanstack/react-query';

export function useCommand(
  keys: any,
  apiFn: QueryFunction<unknown, QueryKey[]>
) {
  const queryClient = useQueryClient();

  const mutation = useMutation(async (payload: any) => await apiFn(payload), {
    onSuccess: async (res) => {
      const keysArray = Array.isArray(keys) ? keys : [keys];
      keysArray.forEach(async (key: QueryKey | undefined) => {
        await queryClient.invalidateQueries(key);
      });
    },
  });

  return {
    execute: (payload: any, options?: any) => mutation.mutate(payload, options),
    isLoading: mutation.isLoading,
    error: mutation.error,
  };
}

export function useQuery(
  key: any[],
  apiFn: QueryFunction<unknown, QueryKey[]>,
  options?: any
) {
  const query = useQueryBase(key, apiFn, options);

  return { ...query };
}
