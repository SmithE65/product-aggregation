#ifndef HASHTABLE_H
#define HASHTABLE_H

#include <stdbool.h>
#include <stddef.h>

typedef struct hashtable hashtable;

hashtable *hashtable_create(void);

void hashtable_free(hashtable *ht);

void *hashtable_get(hashtable *table, const char *key);

const char *hashtable_set(hashtable *table, const char *key, void *value);

size_t hashtable_length(hashtable *table);

typedef struct
{
    const char *key;
    void *value;

    hashtable *_table;
    size_t _index;
} hashtable_iterator;

hashtable_iterator hashtable_get_iterator(hashtable *table);

bool hashtable_next(hashtable_iterator *iterator);

#endif