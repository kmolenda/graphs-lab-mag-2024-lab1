```dot
graph {
        A;
        B;
        C;
        D;
        A -- B;
        A -- C;
        B -- C;
        B -- D;
}
```

```mermaid
graph TD;
    A((A)); B((B)); C((C)); D((D));
    A<-->B;
    A<-->C;
    B<-->C;
    B<-->D;
```
