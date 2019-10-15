import React from "react";
import { Card, CardBody } from "reactstrap";
import faker from "faker";

const FAQ = () => (
  <Card className="mt-4">
    <CardBody className="text-justify p-5">
      <h1>F. A. Q.</h1>
      <section>
        <h3>1. {faker.lorem.words(7)}?</h3>
        <p>{faker.lorem.paragraph(9)}</p>
      </section>
      <section>
        <h3>2. {faker.lorem.words(7)}?</h3>
        <p>{faker.lorem.paragraph(9)}</p>
      </section>
      <section>
        <h3>3. {faker.lorem.words(7)}?</h3>
        <p>{faker.lorem.paragraph(9)}</p>
      </section>
      <section>
        <h3>4. {faker.lorem.words(7)}?</h3>
        <p>{faker.lorem.paragraph(9)}</p>
      </section>
      <section>
        <h3>5. {faker.lorem.words(7)}?</h3>
        <p>{faker.lorem.paragraph(9)}</p>
      </section>
    </CardBody>
  </Card>
);

export default FAQ;
