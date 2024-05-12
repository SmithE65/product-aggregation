#include <assert.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "hashtable.h"

typedef struct
{
    const char *key;
    void *value;
} hashtable_entry;

struct hashtable
{
    hashtable_entry *entries;
    size_t capacity;
    size_t length;
};

#define INITIAL_CAPACITY 16

hashtable *hashtable_create(void)
{
    hashtable *table = malloc(sizeof(hashtable));
    if (table == NULL)
    {
        return NULL;
    }

    table->length = 0;
    table->capacity = INITIAL_CAPACITY;

    table->entries = calloc(table->capacity, sizeof(hashtable_entry));

    if (table->entries == NULL)
    {
        free(table);
        return NULL;
    }

    return table;
}

void hashtable_free(hashtable *table)
{
    for (size_t i = 0; i < table->capacity; i++)
    {
        free((void *)table->entries[i].key);
    }

    free(table->entries);
    free(table);
}

#define FNV_OFFSET 14695981039346656037UL
#define FNV_PRIME 1099511628211UL

static uint64_t hash_key(const char *key)
{
    uint64_t hash = FNV_OFFSET;

    for (const char *p = key; *p; p++)
    {
        hash ^= (uint64_t)(unsigned char)(*p);
        hash *= FNV_PRIME;
    }

    return hash;
}

static size_t get_table_index(const char *key, size_t capacity)
{
    uint64_t hash = hash_key(key);
    size_t index = (size_t)(hash & (uint64_t)(capacity - 1));
    return index;
}

void *hashtable_get(hashtable *table, const char *key)
{
    size_t index = get_table_index(key, table->capacity);

    while (table->entries[index].key != NULL)
    {
        if (strcmp(key, table->entries[index].key) == 0)
        {
            printf("Found at index %li: %s\n", index, key);
            return table->entries[index].value;
        }

        index++;
        if (index >= table->capacity)
        {
            index = 0;
        }
    }

    printf("Key not found: %s\n", key);
    return NULL;
}

static const char *hashtable_set_entry(hashtable_entry *entries, size_t capacity, const char *key, void *value, size_t *plength)
{
    size_t index = get_table_index(key, capacity);
    printf("Attempting to add at index %li\n", index);

    while (entries[index].key != NULL)
    {
        if (strcmp(key, entries[index].key) == 0)
        {
            entries[index].value = value;
            return entries[index].key;
        }

        index++;
        if (index >= capacity)
        {
            index = 0;
        }
    }

    if (plength != NULL)
    {
        key = strdup(key);
        if (key == NULL)
        {
            return NULL;
        }
        (*plength)++;
    }

    printf("Adding to index %li\n", index);
    entries[index].key = key;
    entries[index].value = value;
    return key;
}

static bool hashtable_expand(hashtable *table)
{
    printf("Expanding table\n");
    size_t new_capacity = table->capacity * 2;
    if (new_capacity < table->capacity)
    {
        return false;
    }

    hashtable_entry *new_entries = calloc(new_capacity, sizeof(hashtable_entry));
    if (new_entries == NULL)
    {
        return false;
    }

    for (size_t i = 0; i < table->capacity; i++)
    {
        hashtable_entry entry = table->entries[i];
        if (entry.key != NULL)
        {
            hashtable_set_entry(new_entries, new_capacity, entry.key, entry.value, NULL);
        }
    }

    free(table->entries);
    table->entries = new_entries;
    table->capacity = new_capacity;

    return true;
}

const char *hashtable_set(hashtable *table, const char *key, void *value)
{
    assert(value != NULL);

    if (value == NULL)
    {
        return NULL;
    }

    if (table->length >= table->capacity / 2)
    {
        if (!hashtable_expand(table))
        {
            return NULL;
        }
    }

    return hashtable_set_entry(table->entries, table->capacity, key, value, &table->length);
}

size_t hashtable_length(hashtable *table)
{
    return table->length;
}

hashtable_iterator hashtable_get_iterator(hashtable *table)
{
    hashtable_iterator iterator;
    iterator._table = table;
    iterator._index = 0;
    return iterator;
}

bool hashtable_next(hashtable_iterator *iterator)
{
    hashtable *table = iterator->_table;
    while (iterator->_index < table->capacity)
    {
        size_t i = iterator->_index;
        iterator->_index++;
        if (table->entries[i].key != NULL)
        {
            hashtable_entry entry = table->entries[i];
            iterator->key = entry.key;
            iterator->value = entry.value;
            return true;
        }
    }
    return false;
}