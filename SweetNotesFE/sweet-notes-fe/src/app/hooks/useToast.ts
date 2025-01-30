import toast from 'react-hot-toast';

import { FunctionReturnsVoid } from '@/types';

export function useWarningToast(): FunctionReturnsVoid<[string]> {
  const setMessage = (message: string): void => {
    toast(message, {
      icon: '😔',
      style: {
        borderRadius: '5px',
        background: '#ffd8bf',
        color: '#000000',
      },
    });
  };

  return setMessage;
}

export function useSuccessToast(): FunctionReturnsVoid<[string]> {
  const setMessage = (message: string): void => {
    toast.success(message, {
      style: {
        borderRadius: '5px',
      },
    });
  };

  return setMessage;
}

export function useErrorToast(): FunctionReturnsVoid<[string]> {
  const setMessage = (message: string): void => {
    toast.error(message, {
      style: {
        borderRadius: '5px',
      },
    });
  };

  return setMessage;
}
