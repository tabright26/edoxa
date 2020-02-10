import React, { FunctionComponent, useState } from "react";
import {
  Container,
  Card,
  CardBody,
  CardHeader,
  Button,
  Collapse
} from "reactstrap";
import questions from "./_questions";

const FAQ: FunctionComponent = () => {
  const [accordion, setAccordion] = useState(questions.map(() => false));
  const toggleAccordion = tab => {
    const prevState = accordion;
    const state = prevState.map((x, index) => (tab === index ? !x : false));
    setAccordion(state);
  };
  return (
    <Container>
      <h5 className="text-uppercase my-4">FREQUENTLY ASKED QUESTIONS</h5>
      <div id="accordion">
        {questions.map((question, index) => (
          <Card className="my-1" key={index}>
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
        {/* <Card>
          <CardHeader id="headingOne">
            <Button
              block
              color="link"
              className="text-left m-0 p-0"
              onClick={() => toggleAccordion(0)}
              aria-expanded={accordion[0]}
              aria-controls="collapseOne"
            >
              <h5 className="m-0 p-0">Collapsible Group Item #1</h5>
            </Button>
          </CardHeader>
          <Collapse
            isOpen={accordion[0]}
            data-parent="#accordion"
            id="collapseOne"
            aria-labelledby="headingOne"
          >
            <CardBody>
              1. Anim pariatur cliche reprehenderit, enim eiusmod high life
              accusamus terry richardson ad squid. 3 wolf moon officia aute, non
              cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
              laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
              on it squid single-origin coffee nulla assumenda shoreditch et.
              Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred
              nesciunt sapiente ea proident. Ad vegan excepteur butcher vice
              lomo. Leggings occaecat craft beer farm-to-table, raw denim
              aesthetic synth nesciunt you probably haven't heard of them
              accusamus labore sustainable VHS.
            </CardBody>
          </Collapse>
        </Card>
        <Card>
          <CardHeader id="headingTwo">
            <Button
              block
              color="link"
              className="text-left m-0 p-0"
              onClick={() => toggleAccordion(1)}
              aria-expanded={accordion[1]}
              aria-controls="collapseTwo"
            >
              <h5 className="m-0 p-0">Collapsible Group Item #2</h5>
            </Button>
          </CardHeader>
          <Collapse
            isOpen={accordion[1]}
            data-parent="#accordion"
            id="collapseTwo"
          >
            <CardBody>
              2. Anim pariatur cliche reprehenderit, enim eiusmod high life
              accusamus terry richardson ad squid. 3 wolf moon officia aute, non
              cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
              laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
              on it squid single-origin coffee nulla assumenda shoreditch et.
              Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred
              nesciunt sapiente ea proident. Ad vegan excepteur butcher vice
              lomo. Leggings occaecat craft beer farm-to-table, raw denim
              aesthetic synth nesciunt you probably haven't heard of them
              accusamus labore sustainable VHS.
            </CardBody>
          </Collapse>
        </Card>
        <Card>
          <CardHeader id="headingThree">
            <Button
              block
              color="link"
              className="text-left m-0 p-0"
              onClick={() => toggleAccordion(2)}
              aria-expanded={accordion[2]}
              aria-controls="collapseThree"
            >
              <h5 className="m-0 p-0">Collapsible Group Item #3</h5>
            </Button>
          </CardHeader>
          <Collapse
            isOpen={accordion[2]}
            data-parent="#accordion"
            id="collapseThree"
          >
            <CardBody>
              3. Anim pariatur cliche reprehenderit, enim eiusmod high life
              accusamus terry richardson ad squid. 3 wolf moon officia aute, non
              cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
              laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
              on it squid single-origin coffee nulla assumenda shoreditch et.
              Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred
              nesciunt sapiente ea proident. Ad vegan excepteur butcher vice
              lomo. Leggings occaecat craft beer farm-to-table, raw denim
              aesthetic synth nesciunt you probably haven't heard of them
              accusamus labore sustainable VHS.
            </CardBody>
          </Collapse>
        </Card> */}
      </div>
    </Container>
  );
};

export default FAQ;
