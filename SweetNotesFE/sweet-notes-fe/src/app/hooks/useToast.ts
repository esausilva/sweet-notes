import toast from 'react-hot-toast';

export function useWarningToast(message: string): void {
  toast(message, {
    icon: '😔',
    style: {
      borderRadius: '5px',
      background: '#ffd8bf',
      color: '#000000',
    },
  });
}

export function useSuccessToast(message: string): void {
  toast.success(message, {
    style: {
      borderRadius: '5px',
    },
  });
}

export function useErrorToast(message: string): void {
  toast.error(message, {
    style: {
      borderRadius: '5px',
    },
  });
}
