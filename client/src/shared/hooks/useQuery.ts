import {
  QueryFunction,
  QueryFunctionContext,
  QueryKey,
  useMutation,
  useQuery as useQueryBase,
  useQueryClient,
} from '@tanstack/react-query';

export function useCommand(
  keys: QueryKey[] | QueryKey,
  apiFn: QueryFunction<unknown, QueryKey[]>
) {
  const queryClient = useQueryClient();

  const mutation = useMutation(
    async (payload: QueryFunctionContext<QueryKey[], any>) =>
      await apiFn(payload),
    {
      onSuccess: async (res) => {
        const keysArray = Array.isArray(keys) ? keys : [keys];
        keysArray.forEach(async (key: QueryKey | undefined) => {
          await queryClient.invalidateQueries(key);
        });
      },
    }
  );

  return {
    execute: (payload: QueryFunctionContext<QueryKey[], any>, options?: any) =>
      mutation.mutate(payload, options),
    isLoading: mutation.isLoading,
    error: mutation.error,
  };
}

export function useQuery(
  key: QueryKey[],
  apiFn: QueryFunction<unknown, QueryKey[]>,
  options: any
) {
  const query = useQueryBase(key, apiFn, options);

  return { ...query };
}
