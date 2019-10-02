import React, { useEffect, useState } from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { connectClans } from "store/organizations/clans/container";

import ClansItem from "./ClanItem";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const clans = [
  {
    name: "Big Boi Clan",
    summary: "What a fucking clan."
  },
  {
    name: "Small Boi",
    summary: "What a fucking clan."
  },
  {
    name: "Test Clan 1",
    summary: "This is a summary."
  },
  {
    name: "Test Clan 2",
    summary: "This is a summary."
  },
  {
    name: "Test Clan 3",
    summary: "What a fucking clan."
  },
  {
    name: "Test Clan 4",
    summary: "This is a summary."
  },
  {
    name: "Test Clan 5",
    summary: "What a fucking clan."
  },
  {
    name: "Test Clan 6",
    summary: "This is a summary."
  }
];

const ClansIndex = ({ actions }) => {
  const [searchValue, setSearchValue] = useState("");

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
                  <small class="text-muted">{clans.length} clans and counting !</small>
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
