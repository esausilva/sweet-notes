// using System.Reflection;
// using Data.Config;
// using HotChocolate.Types.Descriptors;
//
// namespace Api.Extensions;
//
// public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
// {
//     protected override void OnConfigure
//     (
//         IDescriptorContext context, 
//         IObjectFieldDescriptor descriptor, 
//         MemberInfo member
//     )
//     {
//         descriptor.UseDbContext<ApplicationDbContext>();
//     }
// }