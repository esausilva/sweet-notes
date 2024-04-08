import { SpecialSomeoneName, ApiResult } from '@/types';

export function FormatSpecialSomeoneName(
  specialSomeoneName: SpecialSomeoneName,
): string {
  return `${specialSomeoneName.firstName} 
  ${specialSomeoneName?.nickname ? `"${specialSomeoneName.nickname}"` : ''} 
  ${specialSomeoneName.lastName}`;
}

export function ShouldReturnNotFound(
  specialSomeone: SpecialSomeoneName | ApiResult,
): boolean {
  return (
    (specialSomeone as ApiResult).status === 400 ||
    (specialSomeone as ApiResult).status === 404
  );
}
