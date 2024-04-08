import { ObjectLiteral, ApiErrorResponse } from '@/types';
import { FetchPost } from '@/helper/networkHelpers';
import { Routes } from '@/constants';

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
