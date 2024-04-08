import { SpecialSomeoneName, ApiErrorResponse } from '@/types';

export function FormatSpecialSomeoneName(
  specialSomeoneName: SpecialSomeoneName,
): string {
  return `${specialSomeoneName.firstName} 
  ${specialSomeoneName?.nickname ? `"${specialSomeoneName.nickname}"` : ''} 
  ${specialSomeoneName.lastName}`;
}

export function ShouldReturnNotFound(
  specialSomeone: SpecialSomeoneName | ApiErrorResponse,
): boolean {
  return (
    (specialSomeone as ApiErrorResponse).status === 400 ||
    (specialSomeone as ApiErrorResponse).status === 404
  );
}
