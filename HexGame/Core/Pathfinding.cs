using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.Core {

    class Node {
        public int f, g, h, x, y;
        public Vector2 coordinates;
        public List<Node> neighbours;
        public Node parent;

        public Node(Vector2 coordinates) {
            this.coordinates = coordinates;
            this.x = (int)coordinates.X;
            this.y = (int)coordinates.Y;
            this.neighbours = new List<Node>();
            this.parent = null;
        }
    }

    class Pathfinding {
        private Node initialNode, goalNode, currentNode;
        private List<Node> openList, closedList, path;
        private Grid hexGrid;

        public Pathfinding(Grid hexGrid) {
            this.hexGrid = hexGrid;
            this.openList = new List<Node>();
            this.closedList = new List<Node>();
            this.path = new List<Node>();
        }

        public List<Hex> findPath(Vector2 initialNodeCoord, Vector2 goalNodeCoord) {
            initialNode = new Node(initialNodeCoord);
            goalNode = new Node(goalNodeCoord);

            initialNode.g = 0;
            initialNode.h = estimateHScore(initialNode, goalNode);
            initialNode.f = initialNode.g + initialNode.h;

            openList.Add(initialNode);

            int tentativeG;
            bool tentativeIsBetter = false;
            Node min;
            currentNode = openList[0];

            while (openList.Count != 0) {
                min = openList[0];
                for (int i = 0; i < openList.Count; i++) {
                    if (openList[i].f <= min.f) {
                        currentNode = openList[i];
                    }
                }

                if (currentNode.coordinates == goalNode.coordinates) {
                    goalNode.parent = currentNode.parent;
                    return convertToHex(reconstructPath(initialNode, goalNode));
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                findNeighbourNodes(currentNode);

                foreach (Node neighbour in currentNode.neighbours) {
                    if (closedList.Any(n => n.coordinates == neighbour.coordinates)) {
                        continue;
                    }

                    tentativeG = currentNode.g + calculateGScore(currentNode, neighbour);

                    if (!openList.Any(n => n.coordinates == neighbour.coordinates)) {
                        openList.Add(neighbour);
                        tentativeIsBetter = true;
                    } else {
                        if (tentativeG < neighbour.g) {
                            tentativeIsBetter = true;
                        } else {
                            tentativeIsBetter = false;
                        }
                    }

                    if (tentativeIsBetter) {
                        neighbour.parent = currentNode;
                        neighbour.g = tentativeG;
                        neighbour.h = estimateHScore(neighbour, goalNode);
                        neighbour.f = neighbour.g + neighbour.h;
                    }

                }
            }
            Console.WriteLine("Error: Couldn't calculate path.");
            return null;
        }

        private List<Node> reconstructPath(Node initialNode, Node goalNode) {
            currentNode = goalNode;
            while (currentNode != null) {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            return path;
        }

        private List<Hex> convertToHex(List<Node> path) {
            List<Hex> hexPath = new List<Hex>();
            foreach (Node node in path) {
                hexPath.Add(new Hex((int)node.coordinates.X, (int)node.coordinates.Y, 0));
            }
            return hexPath;
        }

        private int calculateGScore(Node a, Node b) {
            int deltaX = Math.Abs(a.x - b.x);
            int deltaY = Math.Abs(a.y - b.y);
            double distance = Math.Sqrt(deltaX + deltaY);

            if (distance == Math.Sqrt(2)) {
                return 14;
            } else {
                return 10;
            }
        }

        private int estimateHScore(Node a, Node b) {
            int heuristic = ((int)Math.Abs(a.x - b.x) + (int)Math.Abs(a.y - b.y));
            return heuristic;
        }

        public void findNeighbourNodes(Node node) {
            Hex currentHex = new Hex((int)node.coordinates.X, (int)node.coordinates.Y, 0);

            for(int i = 0; i <= 5; i++) {
                node.neighbours.Add(new Node(new Vector2(
                    Hex.Neighbor(new Hex((int)node.coordinates.X, (int)node.coordinates.Y, 0), i).q,
                    Hex.Neighbor(new Hex((int)node.coordinates.X, (int)node.coordinates.Y, 0), i).r)));
            }
        }
    }
}
