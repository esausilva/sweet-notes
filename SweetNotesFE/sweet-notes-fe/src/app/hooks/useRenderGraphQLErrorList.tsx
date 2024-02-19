import { GraphQLError } from '@/types';

export function useRenderGraphQLErrorList({
  errors,
}: GraphQLError): JSX.Element {
  if (errors.length > 0) {
    return (
      <div className="error-list">
        <ul>
          {errors.map((error, idx) => (
            <li key={`error-${idx}`}>{error.message}</li>
          ))}
        </ul>
      </div>
    );
  } else return <></>;
}
