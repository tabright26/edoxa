import React, { useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import faker from "faker";
import { Translate, withLocalize } from "react-localize-redux";
import locale from "./locale.json";

import "./TermsOfServices.scss";

// https://ryandrewjohnson.github.io/react-localize-redux-docs/
const PageTermsOfServices = ({ addTranslationForLanguage }) => {
  useEffect(() => {
    addTranslationForLanguage(locale, "en");
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Card className="mt-4">
      <CardBody className="text-justify p-5">
        <h1>Terms of Services</h1>
        <p className="text-uppercase">{faker.lorem.paragraph(9)}</p>
        <section>
          <h3>
            <Translate id="hello" />
          </h3>
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
};

export default withLocalize(PageTermsOfServices);
