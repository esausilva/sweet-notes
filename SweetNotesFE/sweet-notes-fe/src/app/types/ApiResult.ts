export type ApiResult = {
  title: string;
  status: number;
  errors?: ApiError;
};

export type ApiError = {
  [key: string]: string[];
};

// TODO: Rename to ApiErrorResponse
