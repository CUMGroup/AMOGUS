# Technical Review - AMOGUS

Performed on 05-28-2023

Meeting start: 13:26

Meeting end: 15:30



Goal: The goal of this meeting is to take a closer look at the three main components of the AMOGUS project in terms of their technical realization. These are the game component in the frontend, the exercise service and the mental exercise factory.



| Component               | Criteria for review      | Why we've chosen this |
| ----------------------- | ------------------------ | ---- |
| game component          | security                 | The main UI component to display the gameplay |
|                         | maintainability          | |
|                         | usablility               | |
| exercise service        | security                 | This service orchestrates the exercise retrieval and generation |
|                         | source code correctness  | |
|                         | performance              | |
| mental exercise factory | source code correctness  | This factory generates random mental exercises |
|                         | well balanced difficulty | |
|                         | performance              | |



## Review Methodology

| Component               | Methodology       |
| ----------------------- | ----------------- |
| game component          | code walkthrough  |
|                         | fromal inspection |
| exercise service        | benchmarks        |
|                         | code walkthrough  |
| mental exercise factory | benchmarks        |
|                         | code walkthrough  |



## Outcome

### Game component

| Action                               | Comment                                                                                                                                             | Responsible    |
| ------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------- | -------------- |
| refactoring code                     | The component has some strange code styles, we need to refactor that                                                                                | Nick Huebner   |
| encode session JSON object to base64 | The JSON object can be spied via the network tab of the browser, what willgive you the results of questions. Therefore we need to encode the object | Jasmin Huebner |



### Exercise Service

| Action          | Comment                                                                                                                           | Responsible    |
| --------------- | --------------------------------------------------------------------------------------------------------------------------------- | -------------- |
| benchmark tests | Benchmarks for different categories and different difficulties (10 questions each), <br/>Benchmarks for time usage and allocation | Alexander Hagl |



### Mental Exercise Factory

| Action          | Comment                                                                                          | Responsible    |
| --------------- | ------------------------------------------------------------------------------------------------ | -------------- |
| benchmark tests | Median try count for million questions, <br/>Time for generate a question,
<br/>Allocation tests | Alexander Hagl |
| refactoring     | The CalcXp and GenerateExpression Methods are very difficult to understand                       | Alexander Hagl |



## Lessions learned

- Security is not that easy to achieve

- Code can easily get complicated to understand (clean code matters)

- external libraries can help to simplify the code

- add website source links to comments if needed
