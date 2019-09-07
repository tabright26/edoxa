import React from "react";
import { Card, CardBody } from "reactstrap";
import faker from "faker";

import "./TermsOfServices.scss";

const PageTermsOfServices = () => (
  <Card className="mt-4">
    <CardBody className="text-justify p-5">
      <h1>Terms of Services</h1>
      <p className="text-uppercase">{faker.lorem.paragraph(9)}</p>
      <section>
        <h3>Section 1</h3>
        <p>{faker.lorem.paragraph(9)}</p>
        <article>
          <h5>Acticle 1</h5>
          <p>{faker.lorem.paragraph(6)}</p>
          <p>{faker.lorem.paragraph(4)}</p>
          <p>{faker.lorem.paragraph(7)}</p>
          <p>{faker.lorem.paragraph(1)}</p>
          <h5>Article 2</h5>
          <p>{faker.lorem.paragraph(6)}</p>
          <p>{faker.lorem.paragraph(9)}</p>
          <p>{faker.lorem.paragraph(9)}</p>
        </article>
        <h3>Section 2</h3>
        <p>{faker.lorem.paragraph(12)}</p>
        <article>
          <h5>Acticle 1</h5>
          <p>{faker.lorem.paragraph(6)}</p>
          <p>{faker.lorem.paragraph(5)}</p>
          <p>{faker.lorem.paragraph(4)}</p>
          <h5>Article 2</h5>
          <p>{faker.lorem.paragraph(2)}</p>
          <p className="mb-0">{faker.lorem.paragraph(3)}</p>
        </article>
      </section>
    </CardBody>
  </Card>
);

export default PageTermsOfServices;
