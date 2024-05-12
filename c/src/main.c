#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "hashtable.h"

int exit_nomem(void)
{
    fprintf(stderr, "Out of memory.  Terminating...\n");
    return -1;
}

int read_line(char *restrict buffer, size_t length)
{
    char *result = fgets(buffer, length - 1, stdin);
    char *p = strchr(buffer, '\n');
    if (p != NULL)
    {
        *p = '\0';
    }
    return result != NULL;
}

int main()
{
    printf("Hello, world!\n%li\n", __STDC_VERSION__);

    printf("Type words one per line, empty line to quit:\n");

    hashtable *table = hashtable_create();
    char buffer[128];
    while (read_line(buffer, 128))
    {
        if (buffer[0] == '\0' || buffer[0] == '\n')
        {
            break;
        }

        void *value = hashtable_get(table, buffer);

        if (value != NULL)
        {
            (*(int *)value)++;
        }
        else
        {
            int *pcount = malloc(sizeof(int));
            if (pcount == NULL)
            {
                return exit_nomem();
            }

            *pcount = 1;

            if (hashtable_set(table, buffer, pcount) == NULL)
            {
                return exit_nomem();
            }
        }
    }

    hashtable_iterator it = hashtable_get_iterator(table);
    while (hashtable_next(&it))
    {
        printf("%s %d\n", it.key, *(int *)it.value);
        free(it.value);
    }

    printf("%d\n", (int)hashtable_length(table));
    hashtable_free(table);
    return 0;
}