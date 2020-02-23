import React, { FunctionComponent } from "react";
import { Container, Card, CardHeader, CardBody } from "reactstrap";
import { items } from "views/Legal/PrivacyPolicy/types";

const PrivacyPolicy: FunctionComponent = () => (
  <Container>
    <h5 className="text-uppercase my-4">Privacy Policy</h5>
    {items.map((item, index) => (
      <Card key={index} className="card-accent-primary text-justify">
        {item.title && (
          <CardHeader className="text-uppercase">{item.title}</CardHeader>
        )}
        <CardBody>{item.content}</CardBody>
      </Card>
    ))}
  </Container>
);

export default PrivacyPolicy;
