import React, { FunctionComponent } from "react";
import { Container, Card, CardBody, CardHeader } from "reactstrap";
import { withLocalize, LocalizeContextProps } from "react-localize-redux";
import { compose } from "recompose";
import { items } from "./types";
//import locale from "./locale.json";

// https://ryandrewjohnson.github.io/react-localize-redux-docs/
const TermsOfUse: FunctionComponent<LocalizeContextProps> = () => {
  // useEffect(() => {
  //   //addTranslationForLanguage(locale, "en");
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, []);
  return (
    <Container>
      <h5 className="text-uppercase my-4">Terms of Use</h5>
      {items.map(({ content, title }, index) => (
        <Card
          key={index}
          className="card-accent-primary text-justify"
        >
          <CardHeader className="text-uppercase">{title}</CardHeader>
          <CardBody>{content}</CardBody>
        </Card>
      ))}
    </Container>
  );
};

const enhance = compose<any, any>(withLocalize);

export default enhance(TermsOfUse);
