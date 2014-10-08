﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.OData.Client
{
    public abstract class MetadataBase : IMetadata
    {
        public abstract string GetEntityCollectionExactName(string collectionName);
        public abstract string GetEntityCollectionTypeName(string collectionName);
        public abstract string GetEntityCollectionTypeNamespace(string collectionName);
        public abstract string GetDerivedEntityTypeExactName(string collectionName, string entityTypeName);
        public abstract bool EntityCollectionTypeRequiresOptimisticConcurrencyCheck(string collectionName);
        public abstract string GetEntityTypeExactName(string entityTypeName);
        public abstract IEnumerable<string> GetStructuralPropertyNames(string entitySetName);
        public abstract bool HasStructuralProperty(string entitySetName, string propertyName);
        public abstract string GetStructuralPropertyExactName(string entitySetName, string propertyName);
        public abstract IEnumerable<string> GetDeclaredKeyPropertyNames(string entitySetName);
        public abstract bool HasNavigationProperty(string entitySetName, string propertyName);
        public abstract string GetNavigationPropertyExactName(string entitySetName, string propertyName);
        public abstract string GetNavigationPropertyPartnerName(string entitySetName, string propertyName);
        public abstract bool IsNavigationPropertyMultiple(string entitySetName, string propertyName);
        public abstract string GetFunctionExactName(string functionName);

        public EntityCollection GetEntityCollection(string collectionName)
        {
            return new EntityCollection(GetEntityCollectionExactName(collectionName));
        }

        public EntityCollection GetBaseEntityCollection(string collectionPath)
        {
            return this.GetEntityCollection(collectionPath.Split('/').First());
        }

        public EntityCollection GetConcreteEntityCollection(string collectionPath)
        {
            var items = collectionPath.Split('/');
            if (items.Count() > 1)
            {
                var baseEntitySet = this.GetEntityCollection(items[0]);
                var entitySet = string.IsNullOrEmpty(items[1])
                    ? baseEntitySet
                    : GetDerivedEntityCollection(baseEntitySet, items[1]);
                return entitySet;
            }
            else
            {
                return this.GetEntityCollection(collectionPath);
            }
        }

        public EntityCollection GetDerivedEntityCollection(EntityCollection baseCollection, string entityTypeName)
        {
            var actualName = GetDerivedEntityTypeExactName(baseCollection.ActualName, entityTypeName);
            return new EntityCollection(actualName, baseCollection);
        }

        public EntryDetails ParseEntryDetails(string collectionName, IDictionary<string, object> entryData, string contentId = null)
        {
            var entryDetails = new EntryDetails();

            foreach (var item in entryData)
            {
                if (this.HasStructuralProperty(collectionName, item.Key))
                {
                    entryDetails.AddProperty(item.Key, item.Value);
                }
                else if (this.HasNavigationProperty(collectionName, item.Key))
                {
                    if (this.IsNavigationPropertyMultiple(collectionName, item.Key))
                    {
                        if (item.Value == null)
                        {
                            entryDetails.AddLink(item.Key, null, contentId);
                        }
                        else
                        {
                            var collection = item.Value as IEnumerable<object>;
                            if (collection != null)
                            {
                                foreach (var element in collection)
                                {
                                    entryDetails.AddLink(item.Key, element, contentId);
                                }
                            }
                        }
                    }
                    else
                    {
                        entryDetails.AddLink(item.Key, item.Value, contentId);
                    }
                }
                else
                {
                    throw new UnresolvableObjectException(item.Key, String.Format("No property or association found for {0}.", item.Key));
                }
            }

            return entryDetails;
        }
    }
}