import React, { useState, useEffect } from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { connectClans } from "store/organizations/clans/container";

import ClansItem from "./ClanItem";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClansIndex = ({ actions, clans }) => {
  const [searchValue, setSearchValue] = useState("");

  useEffect(() => {
    actions.loadClans();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSearchInputChanges = e => {
    setSearchValue(e.target.value);
  };

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h5" className="text-center">
              <Row>
                <Col xs="4" sm="4" md="4">
                  <small className="text-muted">{clans.length} clans and counting !</small>
                </Col>
                <Col xs="4" sm="4" md="4">
                  Clans
                </Col>
                <Col xs="4" sm="4" md="4">
                  Search: <input type="text" value={searchValue} onChange={handleSearchInputChanges} />
                </Col>
              </Row>
            </CardHeader>
          </Card>
        </Col>
      </Row>
      <Row>
        {clans
          .filter(clan => (searchValue ? clan.name.includes(searchValue) : clan))
          .map((clan, index) => (
            <Col key={index} xs="6" sm="4" md="3">
              <ClansItem clan={clan} />
            </Col>
          ))}
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClansIndex);
