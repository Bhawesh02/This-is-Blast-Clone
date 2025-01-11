using System.Collections.Generic;

public abstract class PoolService<T>
{

    private class PooledItem<t>
    {
        public t item;
        public bool isUsed;
    }
    
    private List<PooledItem<T>> m_pooledItems = new();

    public virtual T GetItem()
    {
        if (m_pooledItems.Count > 0)
        {
            PooledItem<T> item = m_pooledItems.Find(x => !x.isUsed);
            if (item != null)
            {
                item.isUsed = true;
                return item.item;
            }
        }
        return CreateNewItem();
    }

    protected T CreateNewItem()
    {
        PooledItem<T> item = new()
        {
            item = CreateItem(),
            isUsed = true
        };
        m_pooledItems.Add(item);
        return item.item;
    }

    protected abstract T CreateItem();

    public virtual void ReturnItem(T item)
    {
        PooledItem<T> usedItem = m_pooledItems.Find(x => x.item.Equals(item));
        usedItem.isUsed = false;
    }

}