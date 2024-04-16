import { useMutation, UseMutationResult } from '@tanstack/react-query';

import { ObjectLiteral, ApiErrorResponse } from '@/types';
import { FetchPost } from '@/helper/networkHelpers';
import { Routes, MutationKeys } from '@/constants';

// NOTE: Deprecated, but leaving here for reference
export async function Authenticate(
  route: Routes,
  body: ObjectLiteral,
): Promise<ApiErrorResponse> {
  try {
    const response: Response = await FetchPost({
      route,
      body,
      fetchOptions: {
        credentials: 'include',
      },
    });

    if (response.status !== 200) {
      const result: ApiErrorResponse = await response.json();
      return result;
    }
  } catch (error) {
    return {
      title: `${error}`,
      status: 500,
      errors: {
        ServerError: [`${error}`],
      },
    };
  }

  return {
    title: '',
    status: 200,
  };
}

export function useAuthService(
  route: Routes,
  mutationKey: MutationKeys,
  body: ObjectLiteral,
): UseMutationResult<Response, ApiErrorResponse, void, unknown> {
  return useMutation({
    mutationKey: [mutationKey],
    mutationFn: async () =>
      await FetchPost({
        route,
        body,
        fetchOptions: {
          credentials: 'include',
        },
      }),
  });
}
