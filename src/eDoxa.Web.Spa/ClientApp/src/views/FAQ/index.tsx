import React, { FunctionComponent, useState, Suspense } from "react";
import {
  Card,
  CardBody,
  CardHeader,
  Button,
  Collapse,
  Row,
  Col,
  ListGroup,
  ListGroupItem
} from "reactstrap";
import {
  questions,
  questionGroups,
  QuestionGroup,
  NEW_USERS_GROUP_ID
} from "./types";
import { LinkContainer } from "react-router-bootstrap";
import { getError404Path, getFaqPath } from "utils/coreui/constants";
import { Loading } from "components/Shared/Loading";
import { Switch, Route, Redirect } from "react-router-dom";
import { RouteProps } from "utils/router/types";

type FaqItemProps = {
  questionGroup: QuestionGroup;
};

const FaqItem: FunctionComponent<FaqItemProps> = ({ questionGroup }) => {
  const items = questions.filter(
    question => question.groupId === questionGroup.id
  );
  const [accordion, setAccordion] = useState(items.map(() => false));
  const toggleAccordion = tab => {
    const prevState = accordion;
    const state = prevState.map((x, index) => (tab === index ? !x : false));
    setAccordion(state);
  };
  return (
    <>
      {items.map((question, index) => (
        <Card
          className={`my-1 ${accordion[index] && "card-accent-primary"}`}
          key={index}
        >
          <CardHeader id={`button-${index}`}>
            <Button
              block
              color="link"
              className={`text-left m-0 p-0 ${
                accordion[index] ? "text-light" : "text-muted"
              }`}
              onClick={() => toggleAccordion(index)}
              aria-expanded={accordion[index]}
              aria-controls={`collapse-${index}`}
            >
              <h5 className="m-0 p-0 text-uppercase">{question.title}</h5>
            </Button>
          </CardHeader>
          <Collapse
            isOpen={accordion[index]}
            data-parent="#accordion"
            id={`collapse-${index}`}
            aria-labelledby={`button-${index}`}
          >
            <CardBody>{question.content}</CardBody>
          </Collapse>
        </Card>
      ))}
    </>
  );
};

const Faq: FunctionComponent = () => {
  return (
    <Row>
      <Col xs="12" sm="12" md="4" lg="3" xl="2">
        <Card className="mt-4">
          <CardHeader>
            <strong className="text-uppercase">Table of content</strong>
          </CardHeader>
          <ListGroup flush>
            {questionGroups.map((questionGroup, index) => (
              <LinkContainer key={index} to={getFaqPath(questionGroup.id)}>
                <ListGroupItem>{questionGroup.name}</ListGroupItem>
              </LinkContainer>
            ))}
          </ListGroup>
        </Card>
      </Col>
      <Col xs="12" sm="12" md="8" lg="7" xl="6">
        <h5 className="text-uppercase my-4">FREQUENTLY ASKED QUESTIONS</h5>
        <Suspense fallback={<Loading />}>
          <Switch>
            {questionGroups.map((questionGroup, index) => (
              <Route<RouteProps>
                key={index}
                path={getFaqPath(questionGroup.id)}
                exact
                name={questionGroup.name}
                render={() => <FaqItem questionGroup={questionGroup} />}
              />
            ))}
            <Redirect from={getFaqPath()} to={getFaqPath(NEW_USERS_GROUP_ID)} />
            <Redirect to={getError404Path()} />
          </Switch>
        </Suspense>
      </Col>
    </Row>
  );
};

export default Faq;
