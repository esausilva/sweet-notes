import { ObjectLiteral, ApiResult } from '@/types';
import { fetchPost } from '@/helper/fetchHelpers';
import { Routes } from '@/constants';

export async function authenticate(
  route: Routes,
  body: ObjectLiteral,
): Promise<ApiResult> {
  try {
    const response: Response = await fetchPost({
      route,
      body,
      fetchOptions: {
        credentials: 'include',
      },
    });

    if (response.status !== 200) {
      const result: ApiResult = await response.json();
      return result;
    }
  } catch (error) {
    console.error(error);
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
