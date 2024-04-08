export type ApiErrorResponse = {
  title: string;
  status: number;
  errors?: ApiError;
};

export type ApiError = {
  [key: string]: string[];
};
