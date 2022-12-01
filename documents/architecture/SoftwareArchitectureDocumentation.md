## 1. Introduction

### 1.1 Purpose

### 1.2 Scope

### 1.3 Definitions, Acronyms and Abbreviations

### 1.4 References

### 1.5 Overview



## 2. Architectural Represantation



## 3. Architectural Goals and Constraints



## 4. Use-Case-View

### 4.1 Use-Case Realizations



## 5. Logical View

### 5.1 Overview

### 5.2 Architecturally Significant Design Packages



## 6. Process View



## 7. Deployment View



## 8. Implementation View

### 8.1 Overview

### 8.2 Layers



## 9. Data View (optional)
Data, such as userdata and session data, will be stored using a database. During development AMOGUS will store it's data in an SQLite and for production MariaDB. 

The original db-concept can be seen [here](https://github.com/CUMGroup/AMOGUS/blob/main/documents/architecture/db_concept.pdf).

Originally the complex questions from a session would also be stored in the database. For simplicity reasons and to reduce data in the database this idea was discarded. The 'Questions'-table (as seen in the original db-concept) is no longer part of the database. 
Instead, the complex questions will be recorded as JSON objects which can easily be modified.


## 10. Size and Performance



## 11. Quality
